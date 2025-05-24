using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natafa.Api.Helper
{
    // EmailTemplates.cs
    public static class EmailTemplates
    {
        public static string SendMailWithLink(string content, string des, string link)
        {
            return @$"
<html xmlns:v=""urn:schemas-microsoft-com:vml"" xmlns:o=""urn:schemas-microsoft-com:office:office"">

<head>
 <meta charset=""UTF-8"" />
 <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
 <!--[if !mso]><!-- -->
 <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"" />
 <!--<![endif]-->
 <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
 <meta name=""format-detection"" content=""telephone=no"" />
 <meta name=""format-detection"" content=""date=no"" />
 <meta name=""format-detection"" content=""address=no"" />
 <meta name=""format-detection"" content=""email=no"" />
 <meta name=""x-apple-disable-message-reformatting"" />
 <link href=""https://fonts.googleapis.com/css?family=Outfit:ital,wght@0,400;0,400;0,500;0,600"" rel=""stylesheet"" />
 <title>SweetSnack</title>
 <!-- Made with Postcards Email Builder by Designmodo -->
 <style>
 html,
         body {{
             margin: 0 !important;
             padding: 0 !important;
             min-height: 100% !important;
             width: 100% !important;
             -webkit-font-smoothing: antialiased;
         }}
 
         * {{
             -ms-text-size-adjust: 100%;
         }}
 
         #outlook a {{
             padding: 0;
         }}
 
         .ReadMsgBody,
         .ExternalClass {{
             width: 100%;
         }}
 
         .ExternalClass,
         .ExternalClass p,
         .ExternalClass td,
         .ExternalClass div,
         .ExternalClass span,
         .ExternalClass font {{
             line-height: 100%;
         }}
 
         table,
         td,
         th {{
             mso-table-lspace: 0 !important;
             mso-table-rspace: 0 !important;
             border-collapse: collapse;
         }}
 
         u + .body table, u + .body td, u + .body th {{
             will-change: transform;
         }}
 
         body, td, th, p, div, li, a, span {{
             -webkit-text-size-adjust: 100%;
             -ms-text-size-adjust: 100%;
             mso-line-height-rule: exactly;
         }}
 
         img {{
             border: 0;
             outline: 0;
             line-height: 100%;
             text-decoration: none;
             -ms-interpolation-mode: bicubic;
         }}
 
         a[x-apple-data-detectors] {{
             color: inherit !important;
             text-decoration: none !important;
         }}
 
         .pc-gmail-fix {{
             display: none;
             display: none !important;
         }}
 
         .body .pc-project-body {{
             background-color: transparent !important;
         }}
 
         @media (min-width: 621px) {{
             .pc-lg-hide {{
                 display: none;
             }} 
 
             .pc-lg-bg-img-hide {{
                 background-image: none !important;
             }}
         }}
 </style>
 <style>
 @media (max-width: 620px) {{
 .pc-project-body {{min-width: 0px !important;}}
 .pc-project-container {{width: 100% !important;}}
 .pc-sm-hide {{display: none !important;}}
 .pc-sm-bg-img-hide {{background-image: none !important;}}
 table.pc-w620-spacing-0-0-28-0 {{margin: 0px 0px 28px 0px !important;}}
 td.pc-w620-spacing-0-0-28-0,th.pc-w620-spacing-0-0-28-0{{margin: 0 !important;padding: 0px 0px 28px 0px !important;}}
 .pc-w620-padding-0-0-0-0 {{padding: 0px 0px 0px 0px !important;}}
 .pc-w620-fontSize-30 {{font-size: 30px !important;}}
 .pc-w620-lineHeight-40 {{line-height: 40px !important;}}
 .pc-w620-fontSize-16 {{font-size: 16px !important;}}
 .pc-w620-lineHeight-26 {{line-height: 26px !important;}}
 .pc-w620-fontSize-14px {{font-size: 14px !important;}}
 .pc-w620-width-auto {{width: auto !important;}}
 .pc-w620-height-auto {{height: auto !important;}}
 .pc-w620-padding-28-28-28-28 {{padding: 28px 28px 28px 28px !important;}}
 table.pc-w620-spacing-0-0-0-0 {{margin: 0px 0px 0px 0px !important;}}
 td.pc-w620-spacing-0-0-0-0,th.pc-w620-spacing-0-0-0-0{{margin: 0 !important;padding: 0px 0px 0px 0px !important;}}
 .pc-w620-fontSize-30px {{font-size: 30px !important;}}
 .pc-w620-itemsSpacings-16-10 {{padding-left: 8px !important;padding-right: 8px !important;padding-top: 5px !important;padding-bottom: 5px !important;}}
 
 .pc-w620-width-fill {{width: 100% !important;}}
 .pc-w620-padding-28-28-0-28 {{padding: 28px 28px 0px 28px !important;}}
 .pc-w620-padding-35-35-35-35 {{padding: 35px 35px 35px 35px !important;}}
 
 .pc-w620-gridCollapsed-1 > tbody,.pc-w620-gridCollapsed-1 > tbody > tr,.pc-w620-gridCollapsed-1 > tr {{display: inline-block !important;}}
 .pc-w620-gridCollapsed-1.pc-width-fill > tbody,.pc-w620-gridCollapsed-1.pc-width-fill > tbody > tr,.pc-w620-gridCollapsed-1.pc-width-fill > tr {{width: 100% !important;}}
 .pc-w620-gridCollapsed-1.pc-w620-width-fill > tbody,.pc-w620-gridCollapsed-1.pc-w620-width-fill > tbody > tr,.pc-w620-gridCollapsed-1.pc-w620-width-fill > tr {{width: 100% !important;}}
 .pc-w620-gridCollapsed-1 > tbody > tr > td,.pc-w620-gridCollapsed-1 > tr > td {{display: block !important;width: auto !important;padding-left: 0 !important;padding-right: 0 !important;margin-left: 0 !important;}}
 .pc-w620-gridCollapsed-1.pc-width-fill > tbody > tr > td,.pc-w620-gridCollapsed-1.pc-width-fill > tr > td {{width: 100% !important;}}
 .pc-w620-gridCollapsed-1.pc-w620-width-fill > tbody > tr > td,.pc-w620-gridCollapsed-1.pc-w620-width-fill > tr > td {{width: 100% !important;}}
 .pc-w620-gridCollapsed-1 > tbody > .pc-grid-tr-first > .pc-grid-td-first,pc-w620-gridCollapsed-1 > .pc-grid-tr-first > .pc-grid-td-first {{padding-top: 0 !important;}}
 .pc-w620-gridCollapsed-1 > tbody > .pc-grid-tr-last > .pc-grid-td-last,pc-w620-gridCollapsed-1 > .pc-grid-tr-last > .pc-grid-td-last {{padding-bottom: 0 !important;}}
 
 .pc-w620-gridCollapsed-0 > tbody > .pc-grid-tr-first > td,.pc-w620-gridCollapsed-0 > .pc-grid-tr-first > td {{padding-top: 0 !important;}}
 .pc-w620-gridCollapsed-0 > tbody > .pc-grid-tr-last > td,.pc-w620-gridCollapsed-0 > .pc-grid-tr-last > td {{padding-bottom: 0 !important;}}
 .pc-w620-gridCollapsed-0 > tbody > tr > .pc-grid-td-first,.pc-w620-gridCollapsed-0 > tr > .pc-grid-td-first {{padding-left: 0 !important;}}
 .pc-w620-gridCollapsed-0 > tbody > tr > .pc-grid-td-last,.pc-w620-gridCollapsed-0 > tr > .pc-grid-td-last {{padding-right: 0 !important;}}
 
 .pc-w620-tableCollapsed-1 > tbody,.pc-w620-tableCollapsed-1 > tbody > tr,.pc-w620-tableCollapsed-1 > tr {{display: block !important;}}
 .pc-w620-tableCollapsed-1.pc-width-fill > tbody,.pc-w620-tableCollapsed-1.pc-width-fill > tbody > tr,.pc-w620-tableCollapsed-1.pc-width-fill > tr {{width: 100% !important;}}
 .pc-w620-tableCollapsed-1.pc-w620-width-fill > tbody,.pc-w620-tableCollapsed-1.pc-w620-width-fill > tbody > tr,.pc-w620-tableCollapsed-1.pc-w620-width-fill > tr {{width: 100% !important;}}
 .pc-w620-tableCollapsed-1 > tbody > tr > td,.pc-w620-tableCollapsed-1 > tr > td {{display: block !important;width: auto !important;}}
 .pc-w620-tableCollapsed-1.pc-width-fill > tbody > tr > td,.pc-w620-tableCollapsed-1.pc-width-fill > tr > td {{width: 100% !important;box-sizing: border-box !important;}}
 .pc-w620-tableCollapsed-1.pc-w620-width-fill > tbody > tr > td,.pc-w620-tableCollapsed-1.pc-w620-width-fill > tr > td {{width: 100% !important;box-sizing: border-box !important;}}
 }}
 @media (max-width: 520px) {{
 .pc-w520-padding-30-30-30-30 {{padding: 30px 30px 30px 30px !important;}}
 }}
 </style>
 <!--[if !mso]><!-- -->
 <style>
 @font-face {{ font-family: 'Outfit'; font-style: normal; font-weight: 400; src: url('https://fonts.gstatic.com/s/outfit/v11/QGYyz_MVcBeNP4NjuGObqx1XmO1I4TC1O4i0FQ.woff') format('woff'), url('https://fonts.gstatic.com/s/outfit/v11/QGYyz_MVcBeNP4NjuGObqx1XmO1I4TC1O4i0Ew.woff2') format('woff2'); }} @font-face {{ font-family: 'Outfit'; font-style: normal; font-weight: 500; src: url('https://fonts.gstatic.com/s/outfit/v11/QGYyz_MVcBeNP4NjuGObqx1XmO1I4QK1O4i0FQ.woff') format('woff'), url('https://fonts.gstatic.com/s/outfit/v11/QGYyz_MVcBeNP4NjuGObqx1XmO1I4QK1O4i0Ew.woff2') format('woff2'); }} @font-face {{ font-family: 'Outfit'; font-style: normal; font-weight: 600; src: url('https://fonts.gstatic.com/s/outfit/v11/QGYyz_MVcBeNP4NjuGObqx1XmO1I4e6yO4i0FQ.woff') format('woff'), url('https://fonts.gstatic.com/s/outfit/v11/QGYyz_MVcBeNP4NjuGObqx1XmO1I4e6yO4i0Ew.woff2') format('woff2'); }}
 </style>
 <!--<![endif]-->
 <!--[if mso]>
    <style type=""text/css"">
        .pc-font-alt {{
            font-family: Arial, Helvetica, sans-serif !important;
        }}
    </style>
    <![endif]-->
 <!--[if gte mso 9]>
    <xml>
        <o:OfficeDocumentSettings>
            <o:AllowPNG/>
            <o:PixelsPerInch>96</o:PixelsPerInch>
        </o:OfficeDocumentSettings>
    </xml>
    <![endif]-->
</head>

<body class=""body pc-font-alt"" style=""width: 100% !important; min-height: 100% !important; margin: 0 !important; padding: 0 !important; line-height: 1.5; color: #2D3A41; mso-line-height-rule: exactly; -webkit-font-smoothing: antialiased; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; font-variant-ligatures: normal; text-rendering: optimizeLegibility; -moz-osx-font-smoothing: grayscale; background-color: #e3dad5;"" bgcolor=""#e3dad5"">
 <table class=""pc-project-body"" style=""table-layout: fixed; min-width: 600px; background-color: #e3dad5;"" bgcolor=""#e3dad5"" width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"">
  <tr>
   <td align=""center"" valign=""top"">
    <table class=""pc-project-container"" align=""center"" width=""600"" style=""width: 600px; max-width: 600px;"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
     <tr>
      <td style=""padding: 20px 0px 20px 0px;"" align=""left"" valign=""top"">
       <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" width=""100%"" style=""width: 100%;"">
        <tr>
         <td valign=""top"">
          <!-- BEGIN MODULE: Header -->
          <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"">
           <tr>
            <td class=""pc-w620-spacing-0-0-0-0"" style=""padding: 0px 0px 0px 0px;"">
             <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"">
              <tr>
               <td valign=""top"" class=""pc-w620-padding-28-28-28-28"" style=""padding: 24px 40px 40px 40px; border-radius: 0px; background-color: #181c2c;"" bgcolor=""#181c2c"">
                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                 <tr>
                  <td class=""pc-w620-spacing-0-0-28-0"" align=""center"" valign=""top"" style=""padding: 0px 0px 32px 0px;"">
                   <!--[if gte mso 12]>
    <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation""><tr><td valign=""top"" style=""border-top: 1px solid #417CD6; border-right: 1px solid #417CD6; border-bottom: 1px solid #417CD6; border-left: 1px solid #417CD6;"">
<![endif]-->
                   <a href=""http://localhost:3000"" style=""text-decoration: none;"">
  <span style=""color: white; font-size: 60px; font-weight: bold;"">Natafa</span>
</a>
                   <!--[if gte mso 12]>
    </td></tr></table>
<![endif]-->
                  </td>
                 </tr>
                </table>
                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                 <tr>
                  <td align=""center"" valign=""top"" style=""padding: 0px 0px 12px 0px;"">
                   <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" width=""100%"" style=""border-collapse: separate; border-spacing: 0; margin-right: auto; margin-left: auto;"">
                    <tr>
                     <td valign=""top"" align=""center"" style=""padding: 0px 0px 0px 0px;"">
                      <div class=""pc-font-alt pc-w620-fontSize-30 pc-w620-lineHeight-40"" style=""line-height: 128%; letter-spacing: -0.2px; font-family: 'Outfit', Arial, Helvetica, sans-serif; font-size: 38px; font-weight: 500; font-variant-ligatures: normal; color: #ffffff; text-align: center; text-align-last: center;"">
                       <div><span>{content}</span>
                       </div>
                      </div>
                     </td>
                    </tr>
                   </table>
                  </td>
                 </tr>
                </table>
                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                 <tr>
                  <td align=""center"" valign=""top"" style=""padding: 0px 0px 12px 0px;"">
                   <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" width=""100%"" style=""border-collapse: separate; border-spacing: 0; margin-right: auto; margin-left: auto;"">
                    <tr>
                     <td valign=""top"" align=""center"" style=""padding: 0px 0px 0px 0px;"">
                      <div class=""pc-font-alt pc-w620-fontSize-16 pc-w620-lineHeight-26"" style=""line-height: 156%; letter-spacing: -0.2px; font-family: 'Outfit', Arial, Helvetica, sans-serif; font-size: 19px; font-weight: normal; font-variant-ligatures: normal; color: #ffffffcc; text-align: center; text-align-last: center;"">
                       <div><span style=""font-weight: 400;font-style: normal;"">{des}</span>
                       </div>
                      </div>
                     </td>
                    </tr>
                   </table>
                  </td>
                 </tr>
                </table>
                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                 <tr>
                  <td align=""center"" style=""padding: 0px 0px 16px 0px;"">
                   <table class=""pc-width-hug pc-w620-gridCollapsed-0"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                    <tr class=""pc-grid-tr-first pc-grid-tr-last"">
                     <td class=""pc-grid-td-first pc-grid-td-last"" valign=""top"" style=""padding-top: 0px; padding-right: 0px; padding-bottom: 0px; padding-left: 0px;"">
                      <table style=""border-collapse: separate; border-spacing: 0;"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                       <tr>
                        <td align=""center"" valign=""top"">
                         <table align=""center"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""width: 100%;"">
                          <tr>
                           <td align=""center"" valign=""top"">
                            <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                             <tr>
                              <th valign=""top"" align=""center"" style=""padding: 0px 0px 0px 0px; text-align: center; font-weight: normal; line-height: 1;"">
                               <!--[if mso]>
        <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" align=""center"" style=""border-collapse: separate; border-spacing: 0; margin-right: auto; margin-left: auto;"">
            <tr>
                <td valign=""middle"" align=""center"" style=""border-radius: 126px 126px 126px 126px; background-color: #ffcb65; text-align:center; color: #ffffff; padding: 12px 24px 12px 24px; mso-padding-left-alt: 0; margin-left:24px;"" bgcolor=""#ffcb65"">
                                    <a class=""pc-font-alt"" style=""display: inline-block; text-decoration: none; font-variant-ligatures: normal; font-family: 'Outfit', Arial, Helvetica, sans-serif; font-weight: 600; font-size: 16px; line-height: 150%; letter-spacing: -0.2px; text-align: center; color: #1a110c;"" href='{link}' target=""_blank""><span style=""display: block;""><span>Confirm</span></span></a>
                                </td>
            </tr>
        </table>
        <![endif]-->
                               <!--[if !mso]><!-- -->
                               <a class=""pc-w620-fontSize-14px"" style=""display: inline-block; box-sizing: border-box; border-radius: 126px 126px 126px 126px; background-color: #ffcb65; padding: 12px 24px 12px 24px; font-family: 'Outfit', Arial, Helvetica, sans-serif; font-weight: 600; font-size: 16px; line-height: 150%; letter-spacing: -0.2px; color: #1a110c; vertical-align: top; text-align: center; text-align-last: center; text-decoration: none; -webkit-text-size-adjust: none;"" href='{link}' target=""_blank""><span style=""display: block;""><span>Confirm</span></span></a>
                               <!--<![endif]-->
                              </th>
                             </tr>
                            </table>
                           </td>
                          </tr>
                         </table>
                        </td>
                       </tr>
                      </table>
                     </td>
                    </tr>
                   </table>
                  </td>
                 </tr>
                </table>
                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                 <tr>
                  <td align=""center"" valign=""top"" style=""padding: 0px 0px 40px 0px;"">
                   <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" class=""pc-w620-width-auto"" width=""100%"" style=""border-collapse: separate; border-spacing: 0; margin-right: auto; margin-left: auto;"">
                    <tr>
                     <td valign=""top"" align=""center"" style=""padding: 0px 0px 0px 0px;"">
                      <div class=""pc-font-alt pc-w620-fontSize-16 pc-w620-lineHeight-26"" style=""line-height: 156%; letter-spacing: -0.2px; font-family: 'Outfit', Arial, Helvetica, sans-serif; font-size: 16px; font-weight: normal; font-variant-ligatures: normal; color: #ffffffb3; text-align: center; text-align-last: center;"">
                       <div><span style=""font-weight: 400;font-style: normal;"">Confirmation available in 5 minutes</span>
                       </div>
                      </div>
                     </td>
                    </tr>
                   </table>
                  </td>
                 </tr>
                </table>
               </td>
              </tr>
             </table>
            </td>
           </tr>
          </table>
          <!-- END MODULE: Header -->
         </td>
        </tr>
        <tr>
         <td valign=""top"">
          <!-- BEGIN MODULE: Contact US -->
          <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"">
           <tr>
            <td class=""pc-w620-spacing-0-0-0-0"" style=""padding: 0px 0px 0px 0px;"">
             <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"">
              <tr>
               <td valign=""top"" class=""pc-w620-padding-28-28-0-28"" style=""padding: 28px 40px 2px 40px; border-radius: 0px; background-color: #ffffff;"" bgcolor=""#ffffff"">
                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                 <tr>
                  <td align=""center"" valign=""top"" style=""padding: 0px 0px 28px 0px;"">
                   <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" width=""100%"" style=""border-collapse: separate; border-spacing: 0; margin-right: auto; margin-left: auto;"">
                    <tr>
                     <td valign=""top"" align=""center"" style=""padding: 0px 0px 0px 0px;"">
                      <div class=""pc-font-alt pc-w620-fontSize-30px"" style=""line-height: 128%; letter-spacing: -0.6px; font-family: 'Outfit', Arial, Helvetica, sans-serif; font-size: 32px; font-weight: 500; font-variant-ligatures: normal; color: #1a110c; text-align: center; text-align-last: center;"">
                       <div><span>Contact Us</span>
                       </div>
                      </div>
                     </td>
                    </tr>
                   </table>
                  </td>
                 </tr>
                </table>
                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                 <tr>
                  <td>
                   <table class=""pc-width-fill pc-w620-gridCollapsed-1 pc-w620-width-fill"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                    <tr class=""pc-grid-tr-first pc-grid-tr-last"">
                     <td class=""pc-grid-td-first pc-w620-itemsSpacings-16-10"" align=""left"" valign=""top"" style=""width: 50%; padding-top: 0px; padding-right: 8px; padding-bottom: 0px; padding-left: 0px;"">
                      <table style=""border-collapse: separate; border-spacing: 0; width: 100%;"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                       <tr>
                        <td align=""left"" valign=""top"" style=""padding: 12px 12px 12px 12px; background-color: #fcedd0; border-radius: 12px 12px 12px 12px;"">
                         <table align=""left"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""width: 100%;"">
                          <tr>
                           <td align=""left"" valign=""top"">
                            <table align=""left"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                             <tr>
                              <td align=""left"" style=""padding: 0px 0px 0px 0px;"">
                               <table class=""pc-width-hug pc-w620-gridCollapsed-0"" align=""left"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                                <tr class=""pc-grid-tr-first pc-grid-tr-last"">
                                 <td class=""pc-grid-td-first"" valign=""middle"" style=""padding-top: 0px; padding-right: 6px; padding-bottom: 0px; padding-left: 0px;"">
                                  <table style=""border-collapse: separate; border-spacing: 0;"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                                   <tr>
                                    <td align=""left"" valign=""top"">
                                     <table align=""left"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""width: 100%;"">
                                      <tr>
                                       <td align=""left"" valign=""top"">
                                        <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                                         <tr>
                                          <td align=""left"" valign=""top"">
                                           <img src=""https://scontent.fsgn1-1.fna.fbcdn.net/v/t1.15752-9/462546892_528717106619598_1499704090279600091_n.png?stp=cp0_dst-png&_nc_cat=109&ccb=1-7&_nc_sid=9f807c&_nc_ohc=KoryII_Bcx8Q7kNvgH9hYFY&_nc_zt=23&_nc_ht=scontent.fsgn1-1.fna&oh=03_Q7cD1QEbzjnmkfFDzg9KCVJ5uTr-UwqHQpIBnCnQjkKrT3kz3w&oe=6755305C"" width=""38"" height=""38"" alt="""" style=""display: block; outline: 0; line-height: 100%; -ms-interpolation-mode: bicubic; width: 38px; height: 38px; border: 0;"" />
                                          </td>
                                         </tr>
                                        </table>
                                       </td>
                                      </tr>
                                     </table>
                                    </td>
                                   </tr>
                                  </table>
                                 </td>
                                 <td class=""pc-grid-td-last"" valign=""middle"" style=""padding-top: 0px; padding-right: 0px; padding-bottom: 0px; padding-left: 6px;"">
                                  <table style=""border-collapse: separate; border-spacing: 0;"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                                   <tr>
                                    <td align=""left"" valign=""top"">
                                     <table align=""left"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""width: 100%;"">
                                      <tr>
                                       <td align=""left"" valign=""top"">
                                        <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" align=""left"" style=""border-collapse: separate; border-spacing: 0;"">
                                         <tr>
                                          <td valign=""top"" align=""left"">
                                           <div class=""pc-font-alt"" style=""line-height: 133%; letter-spacing: -0.2px; font-family: 'Outfit', Arial, Helvetica, sans-serif; font-size: 18px; font-weight: 500; font-variant-ligatures: normal; color: #1b1b1b; text-align: left; text-align-last: left;"">
                                            <div><span>Email Us</span>
                                            </div>
                                           </div>
                                          </td>
                                         </tr>
                                        </table>
                                       </td>
                                      </tr>
                                      <tr>
                                       <td align=""left"" valign=""top"">
                                        <table align=""left"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                                         <tr>
                                          <td valign=""top"" style=""padding: 0px 0px 0px 0px;"">
                                           <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" width=""100%"" style=""border-collapse: separate; border-spacing: 0;"">
                                            <tr>
                                             <td valign=""top"" align=""left"" style=""padding: 0px 0px 0px 0px;"">
                                              <div class=""pc-font-alt"" style=""line-height: 143%; letter-spacing: -0.2px; font-family: 'Outfit', Arial, Helvetica, sans-serif; font-size: 14px; font-weight: normal; font-variant-ligatures: normal; color: #2a1e19; text-align: left; text-align-last: left;"">
                                               <div><span>admin@curcus.store</span>
                                               </div>
                                              </div>
                                             </td>
                                            </tr>
                                           </table>
                                          </td>
                                         </tr>
                                        </table>
                                       </td>
                                      </tr>
                                     </table>
                                    </td>
                                   </tr>
                                  </table>
                                 </td>
                                </tr>
                               </table>
                              </td>
                             </tr>
                            </table>
                           </td>
                          </tr>
                         </table>
                        </td>
                       </tr>
                      </table>
                     </td>
                     <td class=""pc-grid-td-last pc-w620-itemsSpacings-16-10"" align=""left"" valign=""top"" style=""width: 50%; padding-top: 0px; padding-right: 0px; padding-bottom: 0px; padding-left: 8px;"">
                      <table style=""border-collapse: separate; border-spacing: 0; width: 100%;"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                       <tr>
                        <td align=""left"" valign=""top"" style=""padding: 12px 12px 12px 12px; background-color: #fcedd0; border-radius: 12px 12px 12px 12px;"">
                         <table align=""left"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""width: 100%;"">
                          <tr>
                           <td align=""left"" valign=""top"">
                            <table align=""left"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                             <tr>
                              <td align=""left"" style=""padding: 0px 0px 0px 0px;"">
                               <table class=""pc-width-hug pc-w620-gridCollapsed-0"" align=""left"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                                <tr class=""pc-grid-tr-first pc-grid-tr-last"">
                                 <td class=""pc-grid-td-first"" valign=""middle"" style=""padding-top: 0px; padding-right: 6px; padding-bottom: 0px; padding-left: 0px;"">
                                  <table style=""border-collapse: separate; border-spacing: 0;"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                                   <tr>
                                    <td align=""left"" valign=""top"">
                                     <table align=""left"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""width: 100%;"">
                                      <tr>
                                       <td align=""left"" valign=""top"">
                                        <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                                         <tr>
                                          <td align=""left"" valign=""top"">
                                           <img src=""https://scontent.fsgn1-1.fna.fbcdn.net/v/t1.15752-9/462549194_537775149119200_1997646723695623740_n.png?stp=cp0_dst-png&_nc_cat=101&ccb=1-7&_nc_sid=9f807c&_nc_ohc=1LUml1R3nm0Q7kNvgEEPRHq&_nc_zt=23&_nc_ht=scontent.fsgn1-1.fna&oh=03_Q7cD1QGPDbYhlzUBp8qhJkwqg52N5N1Wmwlkb85SEdUg8-0Whw&oe=675523B6"" width=""38"" height=""38"" alt="""" style=""display: block; outline: 0; line-height: 100%; -ms-interpolation-mode: bicubic; width: 38px; height: 38px; border: 0;"" />
                                          </td>
                                         </tr>
                                        </table>
                                       </td>
                                      </tr>
                                     </table>
                                    </td>
                                   </tr>
                                  </table>
                                 </td>
                                 <td class=""pc-grid-td-last"" valign=""middle"" style=""padding-top: 0px; padding-right: 0px; padding-bottom: 0px; padding-left: 6px;"">
                                  <table style=""border-collapse: separate; border-spacing: 0;"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                                   <tr>
                                    <td align=""left"" valign=""top"">
                                     <table align=""left"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""width: 100%;"">
                                      <tr>
                                       <td align=""left"" valign=""top"">
                                        <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" align=""left"" style=""border-collapse: separate; border-spacing: 0;"">
                                         <tr>
                                          <td valign=""top"" align=""left"">
                                           <div class=""pc-font-alt"" style=""line-height: 133%; letter-spacing: -0.2px; font-family: 'Outfit', Arial, Helvetica, sans-serif; font-size: 18px; font-weight: 500; font-variant-ligatures: normal; color: #1b1b1b; text-align: left; text-align-last: left;"">
                                            <div><span>Call Us</span>
                                            </div>
                                           </div>
                                          </td>
                                         </tr>
                                        </table>
                                       </td>
                                      </tr>
                                      <tr>
                                       <td align=""left"" valign=""top"">
                                        <table align=""left"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                                         <tr>
                                          <td valign=""top"" style=""padding: 0px 0px 0px 0px;"">
                                           <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" width=""100%"" style=""border-collapse: separate; border-spacing: 0;"">
                                            <tr>
                                             <td valign=""top"" align=""left"" style=""padding: 0px 0px 0px 0px;"">
                                              <div class=""pc-font-alt"" style=""line-height: 143%; letter-spacing: -0.2px; font-family: 'Outfit', Arial, Helvetica, sans-serif; font-size: 14px; font-weight: normal; font-variant-ligatures: normal; color: #2a1e19; text-align: left; text-align-last: left;"">
                                               <div><span>(+84) 123456789</span>
                                               </div>
                                              </div>
                                             </td>
                                            </tr>
                                           </table>
                                          </td>
                                         </tr>
                                        </table>
                                       </td>
                                      </tr>
                                     </table>
                                    </td>
                                   </tr>
                                  </table>
                                 </td>
                                </tr>
                               </table>
                              </td>
                             </tr>
                            </table>
                           </td>
                          </tr>
                         </table>
                        </td>
                       </tr>
                      </table>
                     </td>
                    </tr>
                   </table>
                  </td>
                 </tr>
                </table>
               </td>
              </tr>
             </table>
            </td>
           </tr>
          </table>
          <!-- END MODULE: Contact US -->
         </td>
        </tr>
        <tr>
         <td valign=""top"">
          <!-- BEGIN MODULE: Referral -->
          <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"">
           <tr>
            <td class=""pc-w620-spacing-0-0-0-0"" style=""padding: 0px 0px 0px 0px;"">
             <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"">
              <tr>
               <td valign=""top"" class=""pc-w620-padding-28-28-28-28"" style=""padding: 28px 40px 40px 40px; border-radius: 0px; background-color: #ffffff;"" bgcolor=""#ffffff"">
               </td>
              </tr>
             </table>
            </td>
           </tr>
          </table>
          <!-- END MODULE: Referral -->
         </td>
        </tr>
        <tr>
         <td valign=""top"">
          <!-- BEGIN MODULE: Footer -->
          <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"">
           <tr>
            <td style=""padding: 0px 0px 0px 0px;"">
             <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"">
              <tr>
               <td valign=""top"" class=""pc-w520-padding-30-30-30-30 pc-w620-padding-35-35-35-35"" style=""padding: 40px 40px 40px 40px; border-radius: 0px; background-color: #181c2c;"" bgcolor=""#181c2c"">
                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                 <tr>
                  <td align=""center"" valign=""top"" style=""padding: 0px 0px 14px 0px;"">
                   <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" width=""100%"" style=""border-collapse: separate; border-spacing: 0; margin-right: auto; margin-left: auto;"">
                    <tr>
                     <td valign=""top"" align=""center"">
                      <div class=""pc-font-alt"" style=""line-height: 143%; letter-spacing: -0.2px; font-family: 'Outfit', Arial, Helvetica, sans-serif; font-size: 14px; font-weight: normal; font-variant-ligatures: normal; color: #ffffffcc; text-align: center; text-align-last: center;"">
                       <div><span style=""color: white;"">Quan 9 Vinhome grand park, S10.06</span></div>
<div><span style=""color: white;"">Bình Dương HT PEARL, A06.17</span></div>
<div><span style=""color: white;"">Khu công nghệ cao Đại học FPT</span></div>
                      </div>
                     </td>
                    </tr>
                   </table>
                  </td>
                 </tr>
                </table>
               </td>
              </tr>
             </table>
            </td>
           </tr>
          </table>
          <!-- END MODULE: Footer -->
         </td>
        </tr>
        <tr>
         <td>
          
         </td>
        </tr>
       </table>
      </td>
     </tr>
    </table>
   </td>
  </tr>
 </table>
 <!-- Fix for Gmail on iOS -->
 <div class=""pc-gmail-fix"" style=""white-space: nowrap; font: 15px courier; line-height: 0;"">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
 </div>
</body>

</html>
";
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static string SendMailWithNoLink(string content, string des)
        {
            return @$"
<html xmlns:v=""urn:schemas-microsoft-com:vml"" xmlns:o=""urn:schemas-microsoft-com:office:office"">

<head>
 <meta charset=""UTF-8"" />
 <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
 <!--[if !mso]><!-- -->
 <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"" />
 <!--<![endif]-->
 <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
 <meta name=""format-detection"" content=""telephone=no"" />
 <meta name=""format-detection"" content=""date=no"" />
 <meta name=""format-detection"" content=""address=no"" />
 <meta name=""format-detection"" content=""email=no"" />
 <meta name=""x-apple-disable-message-reformatting"" />
 <link href=""https://fonts.googleapis.com/css?family=Outfit:ital,wght@0,400;0,400;0,500;0,600"" rel=""stylesheet"" />
 <title>SweetSnack</title>
 <!-- Made with Postcards Email Builder by Designmodo -->
 <style>
 html,
         body {{
             margin: 0 !important;
             padding: 0 !important;
             min-height: 100% !important;
             width: 100% !important;
             -webkit-font-smoothing: antialiased;
         }}
 
         * {{
             -ms-text-size-adjust: 100%;
         }}
 
         #outlook a {{
             padding: 0;
         }}
 
         .ReadMsgBody,
         .ExternalClass {{
             width: 100%;
         }}
 
         .ExternalClass,
         .ExternalClass p,
         .ExternalClass td,
         .ExternalClass div,
         .ExternalClass span,
         .ExternalClass font {{
             line-height: 100%;
         }}
 
         table,
         td,
         th {{
             mso-table-lspace: 0 !important;
             mso-table-rspace: 0 !important;
             border-collapse: collapse;
         }}
 
         u + .body table, u + .body td, u + .body th {{
             will-change: transform;
         }}
 
         body, td, th, p, div, li, a, span {{
             -webkit-text-size-adjust: 100%;
             -ms-text-size-adjust: 100%;
             mso-line-height-rule: exactly;
         }}
 
         img {{
             border: 0;
             outline: 0;
             line-height: 100%;
             text-decoration: none;
             -ms-interpolation-mode: bicubic;
         }}
 
         a[x-apple-data-detectors] {{
             color: inherit !important;
             text-decoration: none !important;
         }}
 
         .pc-gmail-fix {{
             display: none;
             display: none !important;
         }}
 
         .body .pc-project-body {{
             background-color: transparent !important;
         }}
 
         @media (min-width: 621px) {{
             .pc-lg-hide {{
                 display: none;
             }} 
 
             .pc-lg-bg-img-hide {{
                 background-image: none !important;
             }}
         }}
 </style>
 <style>
 @media (max-width: 620px) {{
 .pc-project-body {{min-width: 0px !important;}}
 .pc-project-container {{width: 100% !important;}}
 .pc-sm-hide {{display: none !important;}}
 .pc-sm-bg-img-hide {{background-image: none !important;}}
 table.pc-w620-spacing-0-0-28-0 {{margin: 0px 0px 28px 0px !important;}}
 td.pc-w620-spacing-0-0-28-0,th.pc-w620-spacing-0-0-28-0{{margin: 0 !important;padding: 0px 0px 28px 0px !important;}}
 .pc-w620-padding-0-0-0-0 {{padding: 0px 0px 0px 0px !important;}}
 .pc-w620-fontSize-30 {{font-size: 30px !important;}}
 .pc-w620-lineHeight-40 {{line-height: 40px !important;}}
 .pc-w620-fontSize-16 {{font-size: 16px !important;}}
 .pc-w620-lineHeight-26 {{line-height: 26px !important;}}
 .pc-w620-fontSize-14px {{font-size: 14px !important;}}
 .pc-w620-width-auto {{width: auto !important;}}
 .pc-w620-height-auto {{height: auto !important;}}
 .pc-w620-padding-28-28-28-28 {{padding: 28px 28px 28px 28px !important;}}
 table.pc-w620-spacing-0-0-0-0 {{margin: 0px 0px 0px 0px !important;}}
 td.pc-w620-spacing-0-0-0-0,th.pc-w620-spacing-0-0-0-0{{margin: 0 !important;padding: 0px 0px 0px 0px !important;}}
 .pc-w620-fontSize-30px {{font-size: 30px !important;}}
 .pc-w620-itemsSpacings-16-10 {{padding-left: 8px !important;padding-right: 8px !important;padding-top: 5px !important;padding-bottom: 5px !important;}}
 
 .pc-w620-width-fill {{width: 100% !important;}}
 .pc-w620-padding-28-28-0-28 {{padding: 28px 28px 0px 28px !important;}}
 .pc-w620-padding-35-35-35-35 {{padding: 35px 35px 35px 35px !important;}}
 
 .pc-w620-gridCollapsed-1 > tbody,.pc-w620-gridCollapsed-1 > tbody > tr,.pc-w620-gridCollapsed-1 > tr {{display: inline-block !important;}}
 .pc-w620-gridCollapsed-1.pc-width-fill > tbody,.pc-w620-gridCollapsed-1.pc-width-fill > tbody > tr,.pc-w620-gridCollapsed-1.pc-width-fill > tr {{width: 100% !important;}}
 .pc-w620-gridCollapsed-1.pc-w620-width-fill > tbody,.pc-w620-gridCollapsed-1.pc-w620-width-fill > tbody > tr,.pc-w620-gridCollapsed-1.pc-w620-width-fill > tr {{width: 100% !important;}}
 .pc-w620-gridCollapsed-1 > tbody > tr > td,.pc-w620-gridCollapsed-1 > tr > td {{display: block !important;width: auto !important;padding-left: 0 !important;padding-right: 0 !important;margin-left: 0 !important;}}
 .pc-w620-gridCollapsed-1.pc-width-fill > tbody > tr > td,.pc-w620-gridCollapsed-1.pc-width-fill > tr > td {{width: 100% !important;}}
 .pc-w620-gridCollapsed-1.pc-w620-width-fill > tbody > tr > td,.pc-w620-gridCollapsed-1.pc-w620-width-fill > tr > td {{width: 100% !important;}}
 .pc-w620-gridCollapsed-1 > tbody > .pc-grid-tr-first > .pc-grid-td-first,pc-w620-gridCollapsed-1 > .pc-grid-tr-first > .pc-grid-td-first {{padding-top: 0 !important;}}
 .pc-w620-gridCollapsed-1 > tbody > .pc-grid-tr-last > .pc-grid-td-last,pc-w620-gridCollapsed-1 > .pc-grid-tr-last > .pc-grid-td-last {{padding-bottom: 0 !important;}}
 
 .pc-w620-gridCollapsed-0 > tbody > .pc-grid-tr-first > td,.pc-w620-gridCollapsed-0 > .pc-grid-tr-first > td {{padding-top: 0 !important;}}
 .pc-w620-gridCollapsed-0 > tbody > .pc-grid-tr-last > td,.pc-w620-gridCollapsed-0 > .pc-grid-tr-last > td {{padding-bottom: 0 !important;}}
 .pc-w620-gridCollapsed-0 > tbody > tr > .pc-grid-td-first,.pc-w620-gridCollapsed-0 > tr > .pc-grid-td-first {{padding-left: 0 !important;}}
 .pc-w620-gridCollapsed-0 > tbody > tr > .pc-grid-td-last,.pc-w620-gridCollapsed-0 > tr > .pc-grid-td-last {{padding-right: 0 !important;}}
 
 .pc-w620-tableCollapsed-1 > tbody,.pc-w620-tableCollapsed-1 > tbody > tr,.pc-w620-tableCollapsed-1 > tr {{display: block !important;}}
 .pc-w620-tableCollapsed-1.pc-width-fill > tbody,.pc-w620-tableCollapsed-1.pc-width-fill > tbody > tr,.pc-w620-tableCollapsed-1.pc-width-fill > tr {{width: 100% !important;}}
 .pc-w620-tableCollapsed-1.pc-w620-width-fill > tbody,.pc-w620-tableCollapsed-1.pc-w620-width-fill > tbody > tr,.pc-w620-tableCollapsed-1.pc-w620-width-fill > tr {{width: 100% !important;}}
 .pc-w620-tableCollapsed-1 > tbody > tr > td,.pc-w620-tableCollapsed-1 > tr > td {{display: block !important;width: auto !important;}}
 .pc-w620-tableCollapsed-1.pc-width-fill > tbody > tr > td,.pc-w620-tableCollapsed-1.pc-width-fill > tr > td {{width: 100% !important;box-sizing: border-box !important;}}
 .pc-w620-tableCollapsed-1.pc-w620-width-fill > tbody > tr > td,.pc-w620-tableCollapsed-1.pc-w620-width-fill > tr > td {{width: 100% !important;box-sizing: border-box !important;}}
 }}
 @media (max-width: 520px) {{
 .pc-w520-padding-30-30-30-30 {{padding: 30px 30px 30px 30px !important;}}
 }}
 </style>
 <!--[if !mso]><!-- -->
 <style>
 @font-face {{ font-family: 'Outfit'; font-style: normal; font-weight: 400; src: url('https://fonts.gstatic.com/s/outfit/v11/QGYyz_MVcBeNP4NjuGObqx1XmO1I4TC1O4i0FQ.woff') format('woff'), url('https://fonts.gstatic.com/s/outfit/v11/QGYyz_MVcBeNP4NjuGObqx1XmO1I4TC1O4i0Ew.woff2') format('woff2'); }} @font-face {{ font-family: 'Outfit'; font-style: normal; font-weight: 500; src: url('https://fonts.gstatic.com/s/outfit/v11/QGYyz_MVcBeNP4NjuGObqx1XmO1I4QK1O4i0FQ.woff') format('woff'), url('https://fonts.gstatic.com/s/outfit/v11/QGYyz_MVcBeNP4NjuGObqx1XmO1I4QK1O4i0Ew.woff2') format('woff2'); }} @font-face {{ font-family: 'Outfit'; font-style: normal; font-weight: 600; src: url('https://fonts.gstatic.com/s/outfit/v11/QGYyz_MVcBeNP4NjuGObqx1XmO1I4e6yO4i0FQ.woff') format('woff'), url('https://fonts.gstatic.com/s/outfit/v11/QGYyz_MVcBeNP4NjuGObqx1XmO1I4e6yO4i0Ew.woff2') format('woff2'); }}
 </style>
 <!--<![endif]-->
 <!--[if mso]>
    <style type=""text/css"">
        .pc-font-alt {{
            font-family: Arial, Helvetica, sans-serif !important;
        }}
    </style>
    <![endif]-->
 <!--[if gte mso 9]>
    <xml>
        <o:OfficeDocumentSettings>
            <o:AllowPNG/>
            <o:PixelsPerInch>96</o:PixelsPerInch>
        </o:OfficeDocumentSettings>
    </xml>
    <![endif]-->
</head>

<body class=""body pc-font-alt"" style=""width: 100% !important; min-height: 100% !important; margin: 0 !important; padding: 0 !important; line-height: 1.5; color: #2D3A41; mso-line-height-rule: exactly; -webkit-font-smoothing: antialiased; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; font-variant-ligatures: normal; text-rendering: optimizeLegibility; -moz-osx-font-smoothing: grayscale; background-color: #e3dad5;"" bgcolor=""#e3dad5"">
 <table class=""pc-project-body"" style=""table-layout: fixed; min-width: 600px; background-color: #e3dad5;"" bgcolor=""#e3dad5"" width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"">
  <tr>
   <td align=""center"" valign=""top"">
    <table class=""pc-project-container"" align=""center"" width=""600"" style=""width: 600px; max-width: 600px;"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
     <tr>
      <td style=""padding: 20px 0px 20px 0px;"" align=""left"" valign=""top"">
       <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" width=""100%"" style=""width: 100%;"">
        <tr>
         <td valign=""top"">
          <!-- BEGIN MODULE: Header -->
          <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"">
           <tr>
            <td class=""pc-w620-spacing-0-0-0-0"" style=""padding: 0px 0px 0px 0px;"">
             <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"">
              <tr>
               <td valign=""top"" class=""pc-w620-padding-28-28-28-28"" style=""padding: 24px 40px 40px 40px; border-radius: 0px; background-color: #181c2c;"" bgcolor=""#181c2c"">
                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                 <tr>
                  <td class=""pc-w620-spacing-0-0-28-0"" align=""center"" valign=""top"" style=""padding: 0px 0px 32px 0px;"">
                   <!--[if gte mso 12]>
    <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation""><tr><td valign=""top"" style=""border-top: 1px solid #417CD6; border-right: 1px solid #417CD6; border-bottom: 1px solid #417CD6; border-left: 1px solid #417CD6;"">
<![endif]-->
                   <a href=""http://localhost:3000"" style=""text-decoration: none;"">
  <span style=""color: white; font-size: 60px; font-weight: bold;"">Natafa</span>
</a>
                   <!--[if gte mso 12]>
    </td></tr></table>
<![endif]-->
                  </td>
                 </tr>
                </table>
                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                 <tr>
                  <td align=""center"" valign=""top"" style=""padding: 0px 0px 12px 0px;"">
                   <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" width=""100%"" style=""border-collapse: separate; border-spacing: 0; margin-right: auto; margin-left: auto;"">
                    <tr>
                     <td valign=""top"" align=""center"" style=""padding: 0px 0px 0px 0px;"">
                      <div class=""pc-font-alt pc-w620-fontSize-30 pc-w620-lineHeight-40"" style=""line-height: 128%; letter-spacing: -0.2px; font-family: 'Outfit', Arial, Helvetica, sans-serif; font-size: 38px; font-weight: 500; font-variant-ligatures: normal; color: #ffffff; text-align: center; text-align-last: center;"">
                       <div><span>{content}</span>
                       </div>
                      </div>
                     </td>
                    </tr>
                   </table>
                  </td>
                 </tr>
                </table>
                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                 <tr>
                  <td align=""center"" valign=""top"" style=""padding: 0px 0px 12px 0px;"">
                   <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" width=""100%"" style=""border-collapse: separate; border-spacing: 0; margin-right: auto; margin-left: auto;"">
                    <tr>
                     <td valign=""top"" align=""center"" style=""padding: 0px 0px 0px 0px;"">
                      <div class=""pc-font-alt pc-w620-fontSize-16 pc-w620-lineHeight-26"" style=""line-height: 156%; letter-spacing: -0.2px; font-family: 'Outfit', Arial, Helvetica, sans-serif; font-size: 19px; font-weight: normal; font-variant-ligatures: normal; color: #ffffffcc; text-align: center; text-align-last: center;"">
                       
                      </div>
                     </td>
                    </tr>
                   </table>
                  </td>
                 </tr>
                </table>
                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                 <tr>
                  <td align=""center"" style=""padding: 0px 0px 16px 0px;"">
                   <table class=""pc-width-hug pc-w620-gridCollapsed-0"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                    <tr class=""pc-grid-tr-first pc-grid-tr-last"">
                     <td class=""pc-grid-td-first pc-grid-td-last"" valign=""top"" style=""padding-top: 0px; padding-right: 0px; padding-bottom: 0px; padding-left: 0px;"">
                      <table style=""border-collapse: separate; border-spacing: 0;"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                       <tr>
                        <td align=""center"" valign=""top"">
                         <table align=""center"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""width: 100%;"">
                          <tr>
                           <td align=""center"" valign=""top"">
                            <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                             <tr>
                              <th valign=""top"" align=""center"" style=""padding: 0px 0px 0px 0px; text-align: center; font-weight: normal; line-height: 1;"">
                               <!--[if mso]>
        <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" align=""center"" style=""border-collapse: separate; border-spacing: 0; margin-right: auto; margin-left: auto;"">
            <tr>
                <td valign=""middle"" align=""center"" style=""border-radius: 126px 126px 126px 126px; background-color: #ffcb65; text-align:center; color: #ffffff; padding: 12px 24px 12px 24px; mso-padding-left-alt: 0; margin-left:24px;"" bgcolor=""#ffcb65"">
                                    <div class=""pc-font-alt"" style=""display: inline-block; text-decoration: none; font-variant-ligatures: normal; font-family: 'Outfit', Arial, Helvetica, sans-serif; font-weight: 600; font-size: 16px; line-height: 150%; letter-spacing: -0.2px; text-align: center; color: #1a110c;"" target=""_blank""><span style=""display: block;""><span>{des}</span></span></div>
                                </td>
            </tr>
        </table>
        <![endif]-->
                               <!--[if !mso]><!-- -->
                               <div class=""pc-w620-fontSize-14px"" style=""display: inline-block; box-sizing: border-box; border-radius: 126px 126px 126px 126px; background-color: #ffcb65; padding: 12px 24px 12px 24px; font-family: 'Outfit', Arial, Helvetica, sans-serif; font-weight: 600; font-size: 16px; line-height: 150%; letter-spacing: -0.2px; color: #1a110c; vertical-align: top; text-align: center; text-align-last: center; text-decoration: none; -webkit-text-size-adjust: none;""  target=""_blank""><span style=""display: block;""><span>{des}</span></span></div>
                               <!--<![endif]-->
                              </th>
                             </tr>
                            </table>
                           </td>
                          </tr>
                         </table>
                        </td>
                       </tr>
                      </table>
                     </td>
                    </tr>
                   </table>
                  </td>
                 </tr>
                </table>
                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                 <tr>
                  <td align=""center"" valign=""top"" style=""padding: 0px 0px 40px 0px;"">
                   <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" class=""pc-w620-width-auto"" width=""100%"" style=""border-collapse: separate; border-spacing: 0; margin-right: auto; margin-left: auto;"">
                    <tr>
                     <td valign=""top"" align=""center"" style=""padding: 0px 0px 0px 0px;"">
                      <div class=""pc-font-alt pc-w620-fontSize-16 pc-w620-lineHeight-26"" style=""line-height: 156%; letter-spacing: -0.2px; font-family: 'Outfit', Arial, Helvetica, sans-serif; font-size: 16px; font-weight: normal; font-variant-ligatures: normal; color: #ffffffb3; text-align: center; text-align-last: center;"">
                       
                      </div>
                     </td>
                    </tr>
                   </table>
                  </td>
                 </tr>
                </table>
               </td>
              </tr>
             </table>
            </td>
           </tr>
          </table>
          <!-- END MODULE: Header -->
         </td>
        </tr>
        <tr>
         <td valign=""top"">
          <!-- BEGIN MODULE: Contact US -->
          <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"">
           <tr>
            <td class=""pc-w620-spacing-0-0-0-0"" style=""padding: 0px 0px 0px 0px;"">
             <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"">
              <tr>
               <td valign=""top"" class=""pc-w620-padding-28-28-0-28"" style=""padding: 28px 40px 2px 40px; border-radius: 0px; background-color: #ffffff;"" bgcolor=""#ffffff"">
                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                 <tr>
                  <td align=""center"" valign=""top"" style=""padding: 0px 0px 28px 0px;"">
                   <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" width=""100%"" style=""border-collapse: separate; border-spacing: 0; margin-right: auto; margin-left: auto;"">
                    <tr>
                     <td valign=""top"" align=""center"" style=""padding: 0px 0px 0px 0px;"">
                      <div class=""pc-font-alt pc-w620-fontSize-30px"" style=""line-height: 128%; letter-spacing: -0.6px; font-family: 'Outfit', Arial, Helvetica, sans-serif; font-size: 32px; font-weight: 500; font-variant-ligatures: normal; color: #1a110c; text-align: center; text-align-last: center;"">
                       <div><span>Contact Us</span>
                       </div>
                      </div>
                     </td>
                    </tr>
                   </table>
                  </td>
                 </tr>
                </table>
                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                 <tr>
                  <td>
                   <table class=""pc-width-fill pc-w620-gridCollapsed-1 pc-w620-width-fill"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                    <tr class=""pc-grid-tr-first pc-grid-tr-last"">
                     <td class=""pc-grid-td-first pc-w620-itemsSpacings-16-10"" align=""left"" valign=""top"" style=""width: 50%; padding-top: 0px; padding-right: 8px; padding-bottom: 0px; padding-left: 0px;"">
                      <table style=""border-collapse: separate; border-spacing: 0; width: 100%;"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                       <tr>
                        <td align=""left"" valign=""top"" style=""padding: 12px 12px 12px 12px; background-color: #fcedd0; border-radius: 12px 12px 12px 12px;"">
                         <table align=""left"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""width: 100%;"">
                          <tr>
                           <td align=""left"" valign=""top"">
                            <table align=""left"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                             <tr>
                              <td align=""left"" style=""padding: 0px 0px 0px 0px;"">
                               <table class=""pc-width-hug pc-w620-gridCollapsed-0"" align=""left"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                                <tr class=""pc-grid-tr-first pc-grid-tr-last"">
                                 <td class=""pc-grid-td-first"" valign=""middle"" style=""padding-top: 0px; padding-right: 6px; padding-bottom: 0px; padding-left: 0px;"">
                                  <table style=""border-collapse: separate; border-spacing: 0;"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                                   <tr>
                                    <td align=""left"" valign=""top"">
                                     <table align=""left"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""width: 100%;"">
                                      <tr>
                                       <td align=""left"" valign=""top"">
                                        <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                                         <tr>
                                          <td align=""left"" valign=""top"">
                                           <img src=""https://scontent.fsgn1-1.fna.fbcdn.net/v/t1.15752-9/462546892_528717106619598_1499704090279600091_n.png?stp=cp0_dst-png&_nc_cat=109&ccb=1-7&_nc_sid=9f807c&_nc_ohc=KoryII_Bcx8Q7kNvgH9hYFY&_nc_zt=23&_nc_ht=scontent.fsgn1-1.fna&oh=03_Q7cD1QEbzjnmkfFDzg9KCVJ5uTr-UwqHQpIBnCnQjkKrT3kz3w&oe=6755305C"" width=""38"" height=""38"" alt="""" style=""display: block; outline: 0; line-height: 100%; -ms-interpolation-mode: bicubic; width: 38px; height: 38px; border: 0;"" />
                                          </td>
                                         </tr>
                                        </table>
                                       </td>
                                      </tr>
                                     </table>
                                    </td>
                                   </tr>
                                  </table>
                                 </td>
                                 <td class=""pc-grid-td-last"" valign=""middle"" style=""padding-top: 0px; padding-right: 0px; padding-bottom: 0px; padding-left: 6px;"">
                                  <table style=""border-collapse: separate; border-spacing: 0;"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                                   <tr>
                                    <td align=""left"" valign=""top"">
                                     <table align=""left"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""width: 100%;"">
                                      <tr>
                                       <td align=""left"" valign=""top"">
                                        <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" align=""left"" style=""border-collapse: separate; border-spacing: 0;"">
                                         <tr>
                                          <td valign=""top"" align=""left"">
                                           <div class=""pc-font-alt"" style=""line-height: 133%; letter-spacing: -0.2px; font-family: 'Outfit', Arial, Helvetica, sans-serif; font-size: 18px; font-weight: 500; font-variant-ligatures: normal; color: #1b1b1b; text-align: left; text-align-last: left;"">
                                            <div><span>Email Us</span>
                                            </div>
                                           </div>
                                          </td>
                                         </tr>
                                        </table>
                                       </td>
                                      </tr>
                                      <tr>
                                       <td align=""left"" valign=""top"">
                                        <table align=""left"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                                         <tr>
                                          <td valign=""top"" style=""padding: 0px 0px 0px 0px;"">
                                           <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" width=""100%"" style=""border-collapse: separate; border-spacing: 0;"">
                                            <tr>
                                             <td valign=""top"" align=""left"" style=""padding: 0px 0px 0px 0px;"">
                                              <div class=""pc-font-alt"" style=""line-height: 143%; letter-spacing: -0.2px; font-family: 'Outfit', Arial, Helvetica, sans-serif; font-size: 14px; font-weight: normal; font-variant-ligatures: normal; color: #2a1e19; text-align: left; text-align-last: left;"">
                                               <div><span>admin@curcus.store</span>
                                               </div>
                                              </div>
                                             </td>
                                            </tr>
                                           </table>
                                          </td>
                                         </tr>
                                        </table>
                                       </td>
                                      </tr>
                                     </table>
                                    </td>
                                   </tr>
                                  </table>
                                 </td>
                                </tr>
                               </table>
                              </td>
                             </tr>
                            </table>
                           </td>
                          </tr>
                         </table>
                        </td>
                       </tr>
                      </table>
                     </td>
                     <td class=""pc-grid-td-last pc-w620-itemsSpacings-16-10"" align=""left"" valign=""top"" style=""width: 50%; padding-top: 0px; padding-right: 0px; padding-bottom: 0px; padding-left: 8px;"">
                      <table style=""border-collapse: separate; border-spacing: 0; width: 100%;"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                       <tr>
                        <td align=""left"" valign=""top"" style=""padding: 12px 12px 12px 12px; background-color: #fcedd0; border-radius: 12px 12px 12px 12px;"">
                         <table align=""left"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""width: 100%;"">
                          <tr>
                           <td align=""left"" valign=""top"">
                            <table align=""left"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                             <tr>
                              <td align=""left"" style=""padding: 0px 0px 0px 0px;"">
                               <table class=""pc-width-hug pc-w620-gridCollapsed-0"" align=""left"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                                <tr class=""pc-grid-tr-first pc-grid-tr-last"">
                                 <td class=""pc-grid-td-first"" valign=""middle"" style=""padding-top: 0px; padding-right: 6px; padding-bottom: 0px; padding-left: 0px;"">
                                  <table style=""border-collapse: separate; border-spacing: 0;"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                                   <tr>
                                    <td align=""left"" valign=""top"">
                                     <table align=""left"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""width: 100%;"">
                                      <tr>
                                       <td align=""left"" valign=""top"">
                                        <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                                         <tr>
                                          <td align=""left"" valign=""top"">
                                           <img src=""https://scontent.fsgn1-1.fna.fbcdn.net/v/t1.15752-9/462549194_537775149119200_1997646723695623740_n.png?stp=cp0_dst-png&_nc_cat=101&ccb=1-7&_nc_sid=9f807c&_nc_ohc=1LUml1R3nm0Q7kNvgEEPRHq&_nc_zt=23&_nc_ht=scontent.fsgn1-1.fna&oh=03_Q7cD1QGPDbYhlzUBp8qhJkwqg52N5N1Wmwlkb85SEdUg8-0Whw&oe=675523B6"" width=""38"" height=""38"" alt="""" style=""display: block; outline: 0; line-height: 100%; -ms-interpolation-mode: bicubic; width: 38px; height: 38px; border: 0;"" />
                                          </td>
                                         </tr>
                                        </table>
                                       </td>
                                      </tr>
                                     </table>
                                    </td>
                                   </tr>
                                  </table>
                                 </td>
                                 <td class=""pc-grid-td-last"" valign=""middle"" style=""padding-top: 0px; padding-right: 0px; padding-bottom: 0px; padding-left: 6px;"">
                                  <table style=""border-collapse: separate; border-spacing: 0;"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                                   <tr>
                                    <td align=""left"" valign=""top"">
                                     <table align=""left"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""width: 100%;"">
                                      <tr>
                                       <td align=""left"" valign=""top"">
                                        <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" align=""left"" style=""border-collapse: separate; border-spacing: 0;"">
                                         <tr>
                                          <td valign=""top"" align=""left"">
                                           <div class=""pc-font-alt"" style=""line-height: 133%; letter-spacing: -0.2px; font-family: 'Outfit', Arial, Helvetica, sans-serif; font-size: 18px; font-weight: 500; font-variant-ligatures: normal; color: #1b1b1b; text-align: left; text-align-last: left;"">
                                            <div><span>Call Us</span>
                                            </div>
                                           </div>
                                          </td>
                                         </tr>
                                        </table>
                                       </td>
                                      </tr>
                                      <tr>
                                       <td align=""left"" valign=""top"">
                                        <table align=""left"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                                         <tr>
                                          <td valign=""top"" style=""padding: 0px 0px 0px 0px;"">
                                           <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" width=""100%"" style=""border-collapse: separate; border-spacing: 0;"">
                                            <tr>
                                             <td valign=""top"" align=""left"" style=""padding: 0px 0px 0px 0px;"">
                                              <div class=""pc-font-alt"" style=""line-height: 143%; letter-spacing: -0.2px; font-family: 'Outfit', Arial, Helvetica, sans-serif; font-size: 14px; font-weight: normal; font-variant-ligatures: normal; color: #2a1e19; text-align: left; text-align-last: left;"">
                                               <div><span>(+84) 123456789</span>
                                               </div>
                                              </div>
                                             </td>
                                            </tr>
                                           </table>
                                          </td>
                                         </tr>
                                        </table>
                                       </td>
                                      </tr>
                                     </table>
                                    </td>
                                   </tr>
                                  </table>
                                 </td>
                                </tr>
                               </table>
                              </td>
                             </tr>
                            </table>
                           </td>
                          </tr>
                         </table>
                        </td>
                       </tr>
                      </table>
                     </td>
                    </tr>
                   </table>
                  </td>
                 </tr>
                </table>
               </td>
              </tr>
             </table>
            </td>
           </tr>
          </table>
          <!-- END MODULE: Contact US -->
         </td>
        </tr>
        <tr>
         <td valign=""top"">
          <!-- BEGIN MODULE: Referral -->
          <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"">
           <tr>
            <td class=""pc-w620-spacing-0-0-0-0"" style=""padding: 0px 0px 0px 0px;"">
             <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"">
              <tr>
               <td valign=""top"" class=""pc-w620-padding-28-28-28-28"" style=""padding: 28px 40px 40px 40px; border-radius: 0px; background-color: #ffffff;"" bgcolor=""#ffffff"">
               </td>
              </tr>
             </table>
            </td>
           </tr>
          </table>
          <!-- END MODULE: Referral -->
         </td>
        </tr>
        <tr>
         <td valign=""top"">
          <!-- BEGIN MODULE: Footer -->
          <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"">
           <tr>
            <td style=""padding: 0px 0px 0px 0px;"">
             <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"">
              <tr>
               <td valign=""top"" class=""pc-w520-padding-30-30-30-30 pc-w620-padding-35-35-35-35"" style=""padding: 40px 40px 40px 40px; border-radius: 0px; background-color: #181c2c;"" bgcolor=""#181c2c"">
                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                 <tr>
                  <td align=""center"" valign=""top"" style=""padding: 0px 0px 14px 0px;"">
                   <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" width=""100%"" style=""border-collapse: separate; border-spacing: 0; margin-right: auto; margin-left: auto;"">
                    <tr>
                     <td valign=""top"" align=""center"">
                      <div class=""pc-font-alt"" style=""line-height: 143%; letter-spacing: -0.2px; font-family: 'Outfit', Arial, Helvetica, sans-serif; font-size: 14px; font-weight: normal; font-variant-ligatures: normal; color: #ffffffcc; text-align: center; text-align-last: center;"">
                       <div><span style=""color: white;"">Quan 9 Vinhome grand park, S10.06</span></div>
<div><span style=""color: white;"">Bình Dương HT PEARL, A06.17</span></div>
<div><span style=""color: white;"">Khu công nghệ cao Đại học FPT</span></div>
                      </div>
                     </td>
                    </tr>
                   </table>
                  </td>
                 </tr>
                </table>
               </td>
              </tr>
             </table>
            </td>
           </tr>
          </table>
          <!-- END MODULE: Footer -->
         </td>
        </tr>
        <tr>
         <td>
          
         </td>
        </tr>
       </table>
      </td>
     </tr>
    </table>
   </td>
  </tr>
 </table>
 <!-- Fix for Gmail on iOS -->
 <div class=""pc-gmail-fix"" style=""white-space: nowrap; font: 15px courier; line-height: 0;"">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
 </div>
</body>

</html>
";
        }
    }
}
