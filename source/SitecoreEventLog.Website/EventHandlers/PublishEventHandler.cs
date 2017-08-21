using Sitecore.Data;
using Sitecore.Publishing;
using Sitecore.Publishing.Pipelines.PublishItem;
using SitecoreEventLog.Website.DataAccess.Models;
using SitecoreEventLog.Website.DataAccess.Repositories;
using System;
using System.Collections.Generic;

namespace SitecoreEventLog.Website.EventHandlers
{
    public class PublishEventHandler
    {
        private readonly EventRepository _eventRepository;
        private readonly PublishDetailRepository _publishDetailRepository;
        private PublishDetail _currentPublishDetail;
        private IList<ID> _publishedItems = new List<ID>();
        private DateTime? _publishDate;

        public PublishEventHandler()
        {
            _eventRepository = new EventRepository();
            _publishDetailRepository = new PublishDetailRepository();
        }

        protected void OnItemPublished(object sender, EventArgs args)
        {
            bool isItemProcessedEventArgs = args is ItemProcessedEventArgs;
            if (!isItemProcessedEventArgs)
                return;
            try
            {
                var publishEventArgs = args as ItemProcessedEventArgs;

                if (IsNewPublish(publishEventArgs))
                {
                    SavePublishDetail(publishEventArgs);
                }
                if (EventMustBeSaved(publishEventArgs))
                {
                    SaveEvent(publishEventArgs);
                }
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("EventLog PublishEventHandler threw an exception", ex);
            }
        }

        private void SavePublishDetail(ItemProcessedEventArgs publishEventArgs)
        {
            var publishDetail = CreatePublishDetail(publishEventArgs);
            _currentPublishDetail = _publishDetailRepository.AddPublishDetailItem(publishDetail);
            _publishedItems.Clear();
            _publishDate = publishEventArgs.Context.PublishOptions.PublishDate;
        }

        private void SaveEvent(ItemProcessedEventArgs publishEventArgs)
        {
            var @event = CreatePublishEvent(publishEventArgs);
            _publishedItems.Add(publishEventArgs.Context.ItemId);
            _eventRepository.AddEventItem(@event);
        }

        private bool IsNewPublish(ItemProcessedEventArgs publishEventArgs)
        {
            return _publishDate != publishEventArgs.Context.PublishOptions.PublishDate;
        }

        private bool EventMustBeSaved(ItemProcessedEventArgs publishEventArgs)
        {
            var isRelevantItemPublish = publishEventArgs.Context.PublishOptions.RootItem != null && publishEventArgs.Context.Result.Operation != PublishOperation.Skipped && !_publishedItems.Contains(publishEventArgs.Context.ItemId);
            var isRelevantSitePublish = publishEventArgs.Context.PublishOptions.RootItem == null && publishEventArgs.Context.ItemId == new ID("{11111111-1111-1111-1111-111111111111}") && publishEventArgs.Context.VersionToPublish != null;

            return isRelevantItemPublish || isRelevantSitePublish;
        }

        private PublishDetail CreatePublishDetail(ItemProcessedEventArgs publishEventArgs)
        {
            var publishOptions = publishEventArgs.Context.PublishOptions;

            var rootItemId = publishOptions.RootItem?.ID.ToGuid();
            var rootItemPath = publishOptions.RootItem?.Paths.FullPath ?? "";

            var publishDetailsEntity = new PublishDetail()
            {
                RootItemId = rootItemId,
                Cultures = string.Join(", ", publishEventArgs.Context.PublishContext.Languages),
                IsSitePublish = rootItemId == null,
                RootItemPath = rootItemPath,
                TargetDatabases = publishOptions.TargetDatabase.Name,
                WithRelatedItems = publishOptions.PublishRelatedItems,
                WithSubItems = publishOptions.Deep,
                PublishType = publishOptions.Mode.ToString()
            };

            return publishDetailsEntity;
        }

        private Event CreatePublishEvent(ItemProcessedEventArgs publishEventArgs)
        {
            var eventEntity = new Event()
            {
                PublishDetailId = _currentPublishDetail.Id,
                ItemId = publishEventArgs.Context.ItemId.ToGuid(),
                Date = publishEventArgs.Context.PublishOptions.PublishDate.ToLocalTime(),
                ItemPath = "", //publishEventArgs.Context.ItemName, throws exception
                ItemVersion = publishEventArgs.Context.VersionToPublish?.Version.Number,
                SourceDatabase = publishEventArgs.Context.PublishOptions.SourceDatabase.ConnectionStringName,
                UserName = publishEventArgs.Context.PublishContext.User.Name,
                EventType = EventType.Publish
            };

            return eventEntity;
        }
    }
}