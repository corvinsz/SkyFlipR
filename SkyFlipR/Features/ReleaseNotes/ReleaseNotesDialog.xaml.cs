using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Markdig;

namespace SkyFlipR.Features.ReleaseNotes;

/// <summary>
/// Interaction logic for ReleaseNotesDialog.xaml
/// </summary>
public partial class ReleaseNotesDialog : UserControl
{
    public ReleaseNotesDialog()
    {
        InitializeComponent();
        markdownViewer.Markdown = File.ReadAllText(@"C:\Test\SkyFlipR\SkyFlipR\Features\ReleaseNotes\ReleaseNotes.md");
        //InitializeAsync();
    }
    //async void InitializeAsync()
    //{
    //    await webView.EnsureCoreWebView2Async();

    //    var mdText = File.ReadAllText(@"ReleaseNotes\ReleaseNotes.md");
    //    var htmlBody = Markdown.ToHtml(mdText, new MarkdownPipelineBuilder().UseAdvancedExtensions().Build());

    //    var html = $@"
    //        <!DOCTYPE html>
    //        <html>
    //        <head>
    //          <meta charset=""utf-8"">
    //          <link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/github-markdown-css/github-markdown.css"">
    //          <style>
    //            body {{ box-sizing: border-box; margin: 2em; }}
    //            .markdown-body {{
    //              padding: 16px;
    //            }}
    //          </style>
    //        </head>
    //        <body class=""markdown-body"">
    //          {htmlBody}
    //        </body>
    //        </html>";

    //    webView.NavigateToString(html);
    //}
}
