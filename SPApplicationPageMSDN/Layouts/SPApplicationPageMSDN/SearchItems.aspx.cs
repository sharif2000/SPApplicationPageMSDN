using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.WebControls;
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace SPApplicationPageMSDN.Layouts.SPApplicationPageMSDN
{
    public partial class SearchItems : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(GetItems);
        }

        private void GetItems()
        {
            SPFieldUserValue currentUser = new SPFieldUserValue(this.Web, this.Web.CurrentUser.ID, this.Web.CurrentUser.Name);

            ArrayList resultsList = new ArrayList();

            SPFarm thisFarm = SPFarm.Local;
            SPWebService service = thisFarm.Services.GetValue<SPWebService>("");

            foreach (SPWebApplication webApp in service.WebApplications)
            {
                foreach (SPSite siteCollection in webApp.Sites)
                {
                    foreach (SPWeb web in siteCollection.AllWebs)
                    {
                        SPListCollection lists = web.Lists;
                        foreach (SPList list in lists)
                        {
                            try
                            {
                                resultsList.Add(list.Title);

                                #region I removed this because it's a very large query 
                                //foreach (SPListItem item in list.Items)
                                //{
                                //    if (item[DropDownList1.SelectedValue].ToString() == currentUser.ToString())
                                //    {
                                //        resultsList.Add(item);
                                //    }
                                //} 
                                #endregion
                            }
                            catch (Exception)
                            {
                                // An error with the list. Move onto the next list.
                            }
                        }
                    }
                }
            }

            Label1.Text = "You have " + resultsList.Count +" Lists in this Farm";

            //CreateResultsTable(resultsList);
        }

        //private void CreateResultsTable(ArrayList resultsList)
        //{
        //    string currentList = "";
        //    string currentSite = "";
        //    Table1.Rows.Clear();

        //    foreach (SPListItem item in resultsList)
        //    {
        //        if (item.ParentList.ParentWeb.Title != currentSite)
        //        {
        //            currentSite = item.ParentList.ParentWeb.Title;

        //            TableCell newSiteCell = new TableCell();
        //            newSiteCell.Text = currentSite;

        //            TableRow newSiteRow = new TableRow();
        //            newSiteRow.Cells.Add(newSiteCell);
        //            newSiteRow.Font.Bold = true;
        //            newSiteRow.Font.Size = FontUnit.Larger;
        //            newSiteRow.Font.Underline = true;

        //            Table1.Rows.Add(newSiteRow);
        //        }
        //        if (item.ParentList.Title != currentList)
        //        {
        //            currentList = item.ParentList.Title;
        //            TableCell newListCell = new TableCell();
        //            newListCell.Text = currentList;

        //            TableRow newListRow = new TableRow();
        //            newListRow.Cells.Add(newListCell);
        //            newListRow.Font.Bold = true;

        //            Table1.Rows.Add(newListRow);

        //            TableCell itemHeading = new TableCell();
        //            itemHeading.Text = "Item";
        //            itemHeading.Font.Italic = true;

        //            TableCell createdHeading = new TableCell();
        //            createdHeading.Text = "Created";
        //            createdHeading.Font.Italic = true;

        //            TableCell modifiedHeading = new TableCell();
        //            modifiedHeading.Text = "Last Modified";
        //            modifiedHeading.Font.Italic = true;

        //            TableRow headingRow = new TableRow();
        //            headingRow.Cells.Add(itemHeading);
        //            headingRow.Cells.Add(createdHeading);
        //            headingRow.Cells.Add(modifiedHeading);

        //            Table1.Rows.Add(headingRow);
        //        }

        //        TableCell itemName = new TableCell();
        //        HyperLink itemLink = new HyperLink();
        //        try
        //        {
        //            itemLink.NavigateUrl = item.ParentList.ParentWeb.Url + "/" +
        //            item.ParentList.Forms[PAGETYPE.PAGE_DISPLAYFORM].Url + "?ID=" + item.ID;
        //        }
        //        catch (Exception)
        //        {
        //            // Some items might not have a form page. Ignore the exception.
        //        }
        //        itemLink.Text = item.DisplayName;
        //        itemName.Controls.Add(itemLink);

        //        TableCell created = new TableCell();
        //        created.Text = item["Created"].ToString();

        //        TableCell modified = new TableCell();
        //        modified.Text = item["Modified"].ToString();

        //        TableRow dataRow = new TableRow();
        //        dataRow.Cells.Add(itemName);
        //        dataRow.Cells.Add(created);
        //        dataRow.Cells.Add(modified);

        //        Table1.Rows.Add(dataRow);
        //    }
        //}
    }
}