using System;
using BBQandGrill.Services;

namespace BBQandGrill
{
    public partial class Locations : System.Web.UI.Page
    {
        private readonly LocationService _locationService;

        public Locations()
        {
            _locationService = new LocationService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializePageStyles();
            }
        }

        protected void GetNearLocation(object sender, EventArgs e)
        {
            string searchText = zipText.Text?.Trim();

            LocationSearchResult result = _locationService.SearchLocations(searchText);

            if (result.IsSuccess)
            {
                ShowLocationResults(result.Data);
            }
            else if (result.IsNotFound)
            {
                ShowError(result.Message);
                HideLocationList();
            }
            else
            {
                ShowError(result.Message);
                zipText.Text = string.Empty;
            }
        }

        private void InitializePageStyles()
        {
            errorMessage.CssClass = "textClass";
            zipLabel.CssClass = "textClass";
            zipText.CssClass = "textClass";
            submit.CssClass = "textClass";
            locationDataList.CssClass = "hide";
        }

        private void ShowLocationResults(System.Data.DataSet results)
        {
            locationDataList.DataSource = results;
            locationDataList.DataBind();
            
            errorMessage.CssClass = "hide";
            zipLabel.CssClass = "hide";
            zipText.CssClass = "hide";
            submit.CssClass = "hide";
            locationDataList.CssClass = "show";
        }

        private void HideLocationList()
        {
            locationDataList.CssClass = "hide";
            if (locationResults != null)
            {
                locationResults.Visible = false;
            }
        }

        private void ShowError(string message)
        {
            errorMessage.Text = message;
            errorMessage.CssClass = "errorTextClass show";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _locationService?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}