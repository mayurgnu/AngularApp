using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace AngularApp.HelperClass
{
    public static class CommonExtensions
    {
        public static HttpContext HttpContextAccessor => new HttpContextAccessor().HttpContext;

        // // /// <summary>
        // // /// for render view as a string. when want to stay changes as it as before postback of page.
        // // /// </summary>
        // // /// <param name="controller"></param>
        // // /// <param name="viewName">name of view</param>
        // // /// <param name="model">model of view(if required)</param>
        // // /// <returns></returns>
        // // public static string RenderPartialViewToString(this Controller controller, string viewName, object model, IServiceProvider _service)
        // // {
        // //     if (string.IsNullOrEmpty(viewName))
        // //     {
        // //         viewName = controller.ControllerContext.ActionDescriptor.ActionName;
        // //     }
        // //     controller.ViewData.Model = model;

        // //     using (var sw = new StringWriter())
        // //     {
        // //         var _viewEngine = _service.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
        // //         ViewEngineResult viewResult = null;

        // //         if (viewName.EndsWith(".cshtml"))
        // //         {
        // //             viewResult = _viewEngine.GetView(viewName, viewName, false);
        // //         }
        // //         else
        // //         {
        // //             viewResult = _viewEngine.FindView(controller.ControllerContext, viewName, false);
        // //         }

        // //         ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw, new HtmlHelperOptions());
        // //         var t = viewResult.View.RenderAsync(viewContext);
        // //         t.Wait();

        // //         return sw.GetStringBuilder().ToString();
        // //     }
        // // }

        // // /// <summary>
        // // /// Used to return partial view as string to view engine   
        // // /// </summary>
        // // /// <param name="viewName">partial view name</param>
        // // /// <param name="model">model object used for partial view</param>
        // // /// <param name="_service">Instance of injected services </param>
        // // /// <returns></returns>
        // // public static string RenderViewToString(this Controller controller, string viewName, object model, IServiceProvider _service)
        // // {
        // //     if (string.IsNullOrEmpty(viewName))
        // //     {
        // //         viewName = controller.ControllerContext.ActionDescriptor.ActionName;
        // //     }
        // //     controller.ViewData.Model = model;

        // //     using (var sw = new StringWriter())
        // //     {
        // //         var _viewEngine = _service.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
        // //         ViewEngineResult viewResult = null;

        // //         if (viewName.EndsWith(".cshtml"))
        // //         {
        // //             viewResult = _viewEngine.GetView(viewName, viewName, true);
        // //         }
        // //         else
        // //         {
        // //             viewResult = _viewEngine.FindView(controller.ControllerContext, viewName, true);
        // //         }

        // //         ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw, new HtmlHelperOptions());
        // //         var t = viewResult.View.RenderAsync(viewContext);
        // //         t.Wait();

        // //         return sw.GetStringBuilder().ToString();
        // //     }
        // // }

        /// <summary>
        /// Used to Export excel file with data contained in dt object
        /// Uses OfficeOpenXml package for generating excel file.
        /// </summary>
        /// <param name="dt">Datatable object for which extension method is implemented</param>
        /// <param name="FileName">Filename of excelfile to be returned.</param>
        public static void ExportToExcel(this DataTable dt, string FileName)
        {
            try
            {
                #region worksheet
                ExcelPackage excel = new ExcelPackage();
                var workSheet = excel.Workbook.Worksheets.Add(FileName?.Length == 0 ? "Sheet1" : FileName); // set sheet name

                for (var i = 0; i < dt.Columns.Count; i++)
                {
                    workSheet.Cells[1, i + 1].Value = dt.Columns[i].ColumnName.Replace("_", " ");
                    workSheet.Cells[1, i + 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    workSheet.Cells[1, i + 1].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    workSheet.Cells[1, i + 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    workSheet.Cells[1, i + 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                }

                var headerRwo = workSheet.Row(1);
                headerRwo.Style.Font.SetFromFont(new System.Drawing.Font("Calibri", 11, System.Drawing.FontStyle.Bold));

                workSheet.Row(1).Height = 30;
                int j = 2; //from 2 bcs row one is header
                foreach (DataRow obj in dt.Rows)
                {
                    for (var i = 0; i < dt.Columns.Count; i++)
                    {
                        workSheet.Cells[j, i + 1].Value = Convert.ToString(obj[dt.Columns[i].ColumnName]);
                        workSheet.Cells[j, i + 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        workSheet.Cells[j, i + 1].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        workSheet.Cells[j, i + 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        workSheet.Cells[j, i + 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    }
                    workSheet.Row(j).Height = 30;
                    j++;
                }

                using (var memoryStream = new System.IO.MemoryStream())
                {
                    HttpContextAccessor.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    HttpContextAccessor.Response.Headers.Append("content-disposition", "attachment;  filename=\"" + (FileName?.Length == 0 ? "Excel" : FileName) + ".xlsx\"");
                    excel.SaveAs(memoryStream);
                    HttpContextAccessor.Response.ContentLength = memoryStream.ToArray().Length;
                    HttpContextAccessor.Response.Body.Write(memoryStream.ToArray(), 0, memoryStream.ToArray().Length);
                    HttpContextAccessor.Response.Body.Flush();
                    HttpContextAccessor.Response.Body.Dispose();
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Used to Export word file with data contained in dt object
        /// </summary>
        /// <param name="dt">Datatable object for which extension method is implemented</param>
        /// <param name="FileName">Filename of word file to be returned.</param>

        public static void ExportToWord(this DataTable dt, string FileName)
        {
            try
            {
                StringBuilder html = new StringBuilder();
                html.Append("<table cellpadding='5' border='1' style='border-spacing:0px;'>");
                //add header row
                html.Append("<tr>");
                for (int i = 0; i < dt.Columns.Count; i++)
                    html.Append("<th>").Append(dt.Columns[i].ColumnName).Append("</th>");
                html.Append("</tr>");
                //add rows
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html.Append("<tr>");
                    for (int j = 0; j < dt.Columns.Count; j++)
                        html.Append("<td>").Append(dt.Rows[i][j].ToString()).Append("</td>");
                    html.Append("</tr>");
                }
                html.Append("</table>");

                if (string.IsNullOrEmpty(FileName))
                    FileName = "Word";

                byte[] htmlByte = Encoding.ASCII.GetBytes(html.ToString());

                HttpContextAccessor.Response.Headers.Add("content-disposition", "attachment;filename=\"" + FileName + ".doc\"");
                HttpContextAccessor.Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                HttpContextAccessor.Response.ContentLength = htmlByte.Length;
                HttpContextAccessor.Response.Body.Write(htmlByte, 0, htmlByte.Length);
                HttpContextAccessor.Response.Body.Flush();
                HttpContextAccessor.Response.Body.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Used to Export CSV file with data contained in dt object
        /// </summary>
        /// <param name="dt">Datatable object for which extension method is implemented</param>
        /// <param name="FileName">Filename of CSV file to be returned.</param>

        public static void ExportToCSV(this DataTable dt, string FileName)
        {
            try
            {
                StringBuilder csv = new StringBuilder();
                foreach (DataColumn column in dt.Columns)
                {
                    //Add the Header row for CSV file.
                    csv.Append(column.ColumnName).Append(',');
                }
                //Add new line.
                csv.Append("\r\n");
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        //Add the Data rows.
                        if (column.ColumnName == "Promos")
                        {
                            csv.Append(Regex.Replace(row[column.ColumnName].ToString().Trim() + ',', @"\s+", " "));
                        }
                        else
                        {
                            csv.Append(Regex.Replace(row[column.ColumnName].ToString().Replace(",", " ").Trim() + ',', @"\s+", " "));
                        }
                    }
                    //Add new line.
                    csv.Append("\r\n");
                }

                if (string.IsNullOrEmpty(FileName))
                    FileName = "CommaSeparated";

                byte[] htmlByte = Encoding.ASCII.GetBytes(csv.ToString());

                HttpContextAccessor.Response.Headers.Add("content-disposition", "attachment;filename=" + FileName + ".csv");
                HttpContextAccessor.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContextAccessor.Response.ContentLength = htmlByte.Length;
                HttpContextAccessor.Response.Body.Write(htmlByte, 0, htmlByte.Length);
                HttpContextAccessor.Response.Body.Flush();
                HttpContextAccessor.Response.Body.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Used to Export Pdf file with data contained in dt object
        /// </summary>
        /// <param name="dt">Datatable object for which extension method is implemented</param>
        /// <param name="FileName">Filename of Pdf file to be returned.</param>

        public static void ExportToPdf(this DataTable dt, string FileName)
        {
            try
            {
                StringBuilder html = new StringBuilder();
                html.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>").Append("<head><meta http-equiv='Content-Type' content='text/html; charset=UTF-8'><meta name='viewport' content='width=device-width'><meta http-equiv='X-UA-Compatible' content='IE=edge'>").Append("<title>SamplePdf</title></head><body><table cellpadding='5' border='1' style='border-spacing:0px;'>");
                //add header row
                html.Append("<tr>");
                for (int i = 0; i < dt.Columns.Count; i++)
                    html.Append("<th>").Append(dt.Columns[i].ColumnName).Append("</th>");
                html.Append("</tr>");
                //add rows
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html.Append("<tr>");
                    for (int j = 0; j < dt.Columns.Count; j++)
                        html.Append("<td>").Append(dt.Rows[i][j].ToString()).Append("</td>");
                    html.Append("</tr>");
                }
                html.Append("</table></body></html>");

                if (string.IsNullOrEmpty(FileName))
                    FileName = "Pdf";

                byte[] htmlByte = Encoding.ASCII.GetBytes(html.ToString());

                MemoryStream stream = new MemoryStream();
                stream.Write(htmlByte, 0, htmlByte.Length);
                stream.Position = 0;

                HttpContextAccessor.Response.Headers.Clear();
                HttpContextAccessor.Response.Headers.Add("content-disposition", "attachment;filename=\"" + FileName + ".pdf\"");
                HttpContextAccessor.Response.ContentType = "application/pdf";
                HttpContextAccessor.Response.ContentLength = stream.ToArray().Length;
                HttpContextAccessor.Response.Body.Write(stream.ToArray(), 0, stream.ToArray().Length);
                stream.Dispose();
                HttpContextAccessor.Response.Body.Flush();
                HttpContextAccessor.Response.Body.Dispose();
            }
            catch (Exception ex)
            {
               throw;
            }
        }

        /// <summary>
        /// Used to searialize data of datatable in to json string 
        /// </summary>
        /// <param name="IsSkipTotalRow">Whether to skip column with ColumnName = "totalrows" from display</param>
        /// <returns>Returns json string of serialized data of datatable</returns>
        public static string ConvertToJSON(this DataTable table, Boolean IsSkipTotalRow = true)
        {
            var list = new List<Dictionary<string, object>>();

            foreach (DataRow row in table.Rows)
            {
                var dict = new Dictionary<string, object>();

                foreach (DataColumn col in table.Columns)
                {
                    if (IsSkipTotalRow && !string.Equals(col.ColumnName, "totalrows", StringComparison.OrdinalIgnoreCase))
                        dict[col.ColumnName] = row[col];
                }
                list.Add(dict);
            }
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// Searializes datatable data using  ConvertToJSON() method in to string and appends additional data to that json string
        /// </summary>
        /// <returns>Returns json string of serialized data of datatable</returns>
        public static string GetJsonForDataTableJS(this DataTable dt,DataTableAjaxPostModel model)
        {
            StringBuilder sb = new StringBuilder();
            string data = dt.ConvertToJSON();
            sb.Append("{\"data\":").AppendLine(data);
            sb.Append(",\"draw\":\"").Append(Convert.ToString(model.draw)).Append('\"');
            sb.Append(",\"recordsFiltered\":\"").Append(dt.Rows.Count == 0 ? "0" : dt.Rows[0]["TotalRows"].ToString()).Append('\"');
            sb.Append(",\"recordsTotal\":\"").Append(dt.Rows.Count == 0 ? "0" : dt.Rows[0]["TotalRows"].ToString()).Append("\"}");
            return sb.ToString();
        }

        /// <summary>
        /// Extension Method For Type Object to Convert Object value to int value
        /// </summary>
        /// <returns>returns integer value</returns>
        public static int ToInt(this object a)
        {
            try
            {
                return Convert.ToInt32(a);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Extension Method For Type Object to Convert Object value to decimal value
        /// </summary>
        /// <returns>returns decimal value</returns>
        public static decimal ToDecimal(this object a)
        {
            try
            {
                return Convert.ToDecimal(a);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        // /// <summary>
        // /// Extension Method For Type string to check current string is Base64 string or not
        // /// </summary>
        // /// <returns>returns true or false</returns>
        // public static bool IsBase64(this string base64String)
        // {
        //     if (string.IsNullOrEmpty(base64String) || base64String.Length % 4 != 0
        //     || base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r") || base64String.Contains("\n"))
        //     {
        //         return false;
        //     }

        //     try
        //     {
        //         Convert.FromBase64String(base64String);
        //         return true;
        //     }
        //     catch (Exception ex)
        //     {
        //         ex.SetLog("IsBase64|" + base64String);
        //         // Handle the exception
        //     }
        //     return false;
        // }
    }
}
