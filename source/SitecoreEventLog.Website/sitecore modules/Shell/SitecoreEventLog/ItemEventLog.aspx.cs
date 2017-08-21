using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using SitecoreEventLog.Website.DataAccess;
using SitecoreEventLog.Website.DataAccess.Repositories;
using SitecoreEventLog.Website.DataAccess.Models;

namespace SitecoreEventLog.Website.sitecore_modules.Shell.SitecoreEventLog
{
    public partial class ItemEventLog : System.Web.UI.Page
    {
        private EventRepository EventRepository = new EventRepository();
        private PublishDetailRepository PublishDetailRepository = new PublishDetailRepository();
        private SaveDetailRepository SaveDetailRepository = new SaveDetailRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        
        // TODO - update this method to not use index of cells
        protected void EventLogGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells != null && e.Row.Cells.Count > 1)
            {
                if (e.Row.Cells[1].Text == "Delete")
                {
                    e.Row.Cells[5].Visible = false;
                }
            }
        }
        
        protected void EventLogGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Details")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = EventLogGrid.Rows[index];
                //var publishDetailId = int.Parse(row.Cells[1].Text);
                var dataKey = int.Parse(EventLogGrid.DataKeys[index].Value.ToString());
                var eventType = row.Cells[1].Text;

                if (eventType.Equals("Publish"))
                {
                    List<PublishDetail> publishDetailEntities = new List<PublishDetail>() { PublishDetailRepository.GetPublishDetailItemById(dataKey) };
                    EventLogPublishDetailGrid.DataSource = publishDetailEntities;
                    EventLogPublishDetailGrid.DataBind();

                    ShowPublishDetails();
                }
                else if(eventType.Equals("Save"))
                {
                    List<SaveDetail> saveDetailEntities = new List<SaveDetail>() { SaveDetailRepository.GetSaveDetailItemById(dataKey) };
                    EventLogSaveDetailGrid.DataSource = saveDetailEntities;
                    EventLogSaveDetailGrid.DataBind();

                    ShowSaveDetails();
                }
            }
        }
        
        protected void Back_Click(object sender, EventArgs e)
        {
            ShowEvents();
        }

        private void ShowPublishDetails()
        {
            EventLogGrid.Visible = false;
            btnBack.Visible = true;
            EventLogPublishDetailGrid.Visible = true;
        }

        private void ShowSaveDetails()
        {
            EventLogGrid.Visible = false;
            btnBack.Visible = true;
            EventLogSaveDetailGrid.Visible = true;
        }

        private void ShowEvents()
        {
            EventLogGrid.Visible = true;
            btnBack.Visible = false;
            EventLogPublishDetailGrid.Visible = false;
            EventLogSaveDetailGrid.Visible = false;
        }

        public List<Event> EventLogGrid_GetData(int startRowIndex, int maximumRows, out int totalRowCount)
        {
            var itemId = HttpUtility.UrlDecode(Request.QueryString["id"]);
            List<Event> events = EventRepository.GetEventsByItemId(new Guid(itemId), maximumRows, startRowIndex);
            totalRowCount = EventRepository.CountEventsByItemId(new Guid(itemId));
            return events;
        }
    }
}