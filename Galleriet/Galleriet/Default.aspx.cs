using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Galleriet.Model;

namespace Galleriet
{
    public partial class Default : System.Web.UI.Page
    {
        private Gallery _gallery;

        private Gallery Gallery {
            get { return _gallery ?? (_gallery = new Gallery()); }
        }

        private string Message
        {
            get { return Session["Message"] as string; }
            set
            {
                Session["Message"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["name"] != null)
            {
                ImageFull.ImageUrl = String.Format("Images/{0}", Request.QueryString["name"]);
                FullImageWrapper.Visible = true;
            }

            if (Message != null)
            {
                PanelSuccess.Visible = true;
                var messageString = Message;
                LabelSuccess.Text += messageString;
                Message = messageString;
                Session.Clear();
            }
        }

        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (FileUpload.HasFile)
                {
                    try
                    {
                        var image = Gallery.SaveImage(FileUpload.FileContent, FileUpload.FileName);
                        var messageString = String.Format("Bilden '{0}' har laddats upp!", image);
                        Message = messageString;
                        Response.Redirect(String.Format("?name={0}", image));
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(String.Empty, ex.Message);
                    }
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "En fil måste väljas.");
                }
            }
        }

        public IEnumerable<string> ThumbRepeater_GetData()
        {
            return Gallery.GetImagesNames();
        }
    }
}