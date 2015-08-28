<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AspWebsite.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/website.css" rel="stylesheet" />
</head>
<body>
    <form action="/Upload.ashx" class="dropzone" id="my-awesome-dropzone"></form>
    <input type="text" name="imageId" id="imageId" />
    <button type="button" id="refreshButton">Update Image</button>
    <img src="/" style="display: none" id="preview" />

    <script src="http://code.jquery.com/jquery-1.10.1.min.js"></script>
    <script src="Scripts/dropzone.js"></script>

        <script type="text/javascript">

            $(document).ready(function () {
                $("#preview").fadeOut(15);
                $("#refreshButton").click(function () {
                    var imageToLoad = $("#imageId").val();
                    if (imageToLoad.length > 0) {
                        $("#preview").attr("src", "/Download.ashx?id=" + imageToLoad);
                        $("#preview").fadeIn();
                    }
                });
            });

    </script>
</body>
</html>
