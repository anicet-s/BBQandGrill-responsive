using System;

namespace BBQandGrill
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Log error details here when logging is implemented (Phase 4)
            Exception lastError = Server.GetLastError();
            
            if (lastError != null)
            {
                // TODO: Log to file or monitoring service
                // For now, just clear the error
                Server.ClearError();
            }
        }
    }
}
