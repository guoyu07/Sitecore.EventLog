using Newtonsoft.Json;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Events;
using SitecoreEventLog.Website.Configuration;
using SitecoreEventLog.Website.DataAccess.Models;
using SitecoreEventLog.Website.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SitecoreEventLog.Website.EventHandlers
{
    public class SaveEventHandler
    {
        private readonly EventRepository _eventRepository;
        private readonly SaveDetailRepository _saveDetailsRepository;

        private readonly ID _ownerFieldId = new ID("{52807595-0F8F-4B20-8D2A-CB71D28C6103}");
        private readonly ID _createdFieldId = new ID("{25BED78C-4957-4165-998A-CA1B52F67497}");        
        private readonly ID _createdByFieldId = new ID("{5DD74568-4D4B-44C1-B513-0AF5F4CDA34F}");        
        private readonly ID _updatedFieldId = new ID("{D9CF14B1-FA16-4BA6-9288-E8A174D4D522}");
        private readonly ID _updatedByFieldId = new ID("{BADD9CF9-53E0-4D0C-BCC0-2D784C282F6A}");
        private readonly ID _revisionFieldId = new ID("{8CDC337E-A112-42FB-BBB4-4143751E123F}");
        private readonly ID _lockFieldId = new ID("{001DD393-96C5-490B-924A-B0F25CD9EFD8}");
        
        private readonly ID _renderingsFieldId = new ID("{F1A1FE9E-A60C-4DDB-A3A0-BB5B29FE732E}");
        private readonly ID _finalRenderingsFieldId = new ID("{04BF00DB-F5FB-41F7-8AB7-22408372A981}");

        private readonly List<ID> IgnoredFields = new List<ID>();
        public SaveEventHandler()
        {
            _eventRepository = new EventRepository();
            _saveDetailsRepository = new SaveDetailRepository();

            IgnoredFields.Add(_ownerFieldId);
            IgnoredFields.Add(_createdFieldId);
            IgnoredFields.Add(_createdByFieldId);
            IgnoredFields.Add(_updatedFieldId);
            IgnoredFields.Add(_updatedByFieldId);
            IgnoredFields.Add(_revisionFieldId);
            IgnoredFields.Add(_lockFieldId);
        }

        public void OnItemSaved(object sender, EventArgs args)
        {
            bool isItemSavedEventArgs = args is SitecoreEventArgs;
            if (!isItemSavedEventArgs)
                return;

            var saveEventArgs = args as SitecoreEventArgs;
            var itemChanges = (ItemChanges)saveEventArgs.Parameters[saveEventArgs.Parameters.Length - 1];
            bool isSaveEventInvokedByPublish = itemChanges.HasPropertiesChanged;

            if (isSaveEventInvokedByPublish)
                return;
            try
            {
                var saveDetailsEntity = new SaveDetail()
                {
                    Language = itemChanges.Item.Language.Name,
                    Changes = Settings.TraceChanges ? GetFieldChanges(itemChanges) : "[not-tracked]"
                };

                var _saveDetails = _saveDetailsRepository.AddSaveDetailItem(saveDetailsEntity);

                var eventEntity = new DataAccess.Models.Event()
                {
                    ItemId = itemChanges.Item.ID.ToGuid(),
                    Date = DateTime.ParseExact(itemChanges.FieldChanges[_updatedFieldId].Value, "yyyyMMddTHHmmssZ", CultureInfo.InvariantCulture), // TODO
                    EventType = EventType.Save,
                    ItemPath = itemChanges.Item.Paths?.FullPath,
                    ItemVersion = itemChanges.Item.Version.Number,
                    SourceDatabase = itemChanges.Item.Database.Name,
                    UserName = itemChanges.FieldChanges[_updatedByFieldId].Value,
                    SaveDetailId = _saveDetails.Id
                };

                _eventRepository.AddEventItem(eventEntity);
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("EventLog SaveEventHandler threw an exception", ex);
            }
        }

        public string GetFieldChanges(ItemChanges itemChanges)
        {
            var fieldChanges = new FieldChanges();

            if (itemChanges != null && itemChanges.FieldChanges != null)
            {
                foreach (FieldChange field in itemChanges.FieldChanges)
                {
                    if (!IgnoredFields.Contains(field.FieldID))
                    {
                        fieldChanges.Changes.Add(new Change()
                        {
                            FieldId = field.FieldID.Guid,
                            FieldName = field.Definition.Name,
                            OldValue = Settings.TraceOldvalue ? TruncateValue(field.FieldID, field.OriginalValue) : "[not-tracked]",
                            NewValue = Settings.TraceNewValue ? TruncateValue(field.FieldID, field.Value) : "[not-tracked]"
                        });
                    }
                }

                if (fieldChanges.Changes.Any())
                {
                    var s = JsonConvert.SerializeObject(fieldChanges);
                    return s;
                }
            }
                        
            return string.Empty;
        }

        public string TruncateValue(ID fieldID, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            else if (fieldID.Equals(_renderingsFieldId) || fieldID.Equals(_finalRenderingsFieldId))
            {
                return value;
            }
            else if (Settings.TraceValueMaxLength > 0 && value.Length > Settings.TraceValueMaxLength)
            {
                return string.Format("{0}...[TRUNCATED]", value.Substring(0, Settings.TraceValueMaxLength));
            }

            return value;
        }
    }
}