using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using Helpers.Net.Resource;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("Current Culture: " + Thread.CurrentThread.CurrentUICulture);
    }

    protected override void InitializeCulture()
    {
        if (Session["culture"] != null)
        {
            String selectedLanguage = Session["culture"].ToString();

            Page.UICulture = selectedLanguage;
            Page.Culture = selectedLanguage;
        }
        base.InitializeCulture();
    }
    protected void English_Click(object sender, EventArgs e)
    {
        Session["culture"] = "en-US";
        Response.Redirect("~/");
    }

    protected void French_Click(object sender, EventArgs e)
    {
        Session["culture"] = "fr-FR";
        Response.Redirect("~/");
    }

    protected void Nepali_Click(object sender, EventArgs e)
    {
        Session["culture"] = "ne-NP";
        Response.Redirect("~/");
    }

    protected void ClearResourceCache_Click(object sender, EventArgs e)
    {
        SQLiteResourceProviderFactory.ClearCache();
    }
}