<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Galleriet.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/app.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="Scripts/app.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <h1><a href="Default.aspx">Galleriet</a></h1>

            <asp:Panel ID="PanelSuccess" runat="server" Visible="false">
                <asp:Label ID="LabelSuccess" class="alert" runat="server" Text="">
                    <a class="close" href="#">&times;</a>
                </asp:Label>
            </asp:Panel>

            <div ID="FullImageWrapper" class="full-image" runat="server" visible="false">
                <asp:PlaceHolder ID="ImagePlaceHolder" runat="server">
                    <asp:Image ID="ImageFull" runat="server" />
                </asp:PlaceHolder>
            </div>

            <fieldset>
                <legend>Ladda upp bild</legend>
                <div>
                    <asp:FileUpload ID="FileUpload" runat="server" />
                    <asp:RequiredFieldValidator
                        ID="RequiredFileUploadValidator"
                        ControlToValidate="FileUpload"
                        runat="server"
                        CssClass="errors"
                        ErrorMessage="En fil måste väljas."
                        Text="*">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator
                        ID="RegularExpressionValidator1"
                        ControlToValidate="FileUpload"
                        ValidationExpression="^.*\.(gif|jpg|png)$"
                        runat="server"
                        CssClass="errors"
                        ErrorMessage="Endast bilder av typerna GIF, JPEG eller PNG är tillåtna."
                        Text="*">
                    </asp:RegularExpressionValidator>
                    <asp:Button ID="ButtonUpload" runat="server" Text="Ladda upp" OnClick="ButtonUpload_Click" />
                </div>

            </fieldset>
            <div>
                <asp:ValidationSummary
                    ID="errorList"
                    CssClass="alert"
                    DisplayMode="BulletList"
                    EnableClientScript="true"
                    HeaderText="Fel inträffade. Åtgärda felen och försök igen."
                    runat="server" />
            </div>

            <div class="image-wrap">
                <asp:Repeater
                    ID="ThumbRepeater"
                    runat="server"
                    ItemType="System.String"
                    SelectMethod="ThumbRepeater_GetData">
                    <ItemTemplate>
                        <div class="image">
                            <asp:HyperLink ID="HyperLinkImage" NavigateUrl='<%# String.Format("?name={0}", Item) %>' runat="server">
                                <asp:Image ID="Image" ImageUrl='<%# String.Format("Images/Thumbs/{0}", Item) %>' runat="server" />
                            </asp:HyperLink>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <footer class="footer">
                <p>ASP.NET Web Forms (1DV406) > Steg 2 > Laborationsuppgift 1</p>
            </footer>
        </div>
    </form>
</body>
</html>
