using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShoppingCartView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //TODO: Add your code here

        ShoppingCart shoppingCart = (ShoppingCart)Session["shoppingCart"];

        if (shoppingCart.NumOfItems == 0)
        {
            TableRow row = new TableRow();
            tblShoppingCart.Rows.Add(row);

            TableCell varningCell = new TableCell();
            row.Cells.Add(varningCell);

            varningCell.Text = "Your shopping cart is empty";
            varningCell.ForeColor = System.Drawing.Color.Red;
            varningCell.ColumnSpan = 3;
            varningCell.HorizontalAlign = HorizontalAlign.Center;
        }
        else
        {
            int numOfRows = tblShoppingCart.Rows.Count;

            if (numOfRows >= 1)
            {
                for (int item = numOfRows - 1; item > 0; item--)
                {
                    tblShoppingCart.Rows.RemoveAt(item);
                }

                foreach (BookOrder each in shoppingCart.BookOrders)
                {
                    TableRow rows = new TableRow();
                    TableCell cells = new TableCell();

                    cells.Text = each.Book.Title.ToString();
                    rows.Cells.Add(cells);

                    cells = new TableCell();
                    cells.Text = each.NumOfCopies.ToString();
                    rows.Controls.Add(cells);

                    double finalBookprice = each.NumOfCopies * each.Book.Price;

                    cells = new TableCell();
                    cells.Text = finalBookprice.ToString();
                    rows.Controls.Add(cells);

                    tblShoppingCart.Controls.Add(rows);
                }


                // total row
                TableRow row = new TableRow();

                TableCell totalCell = new TableCell();
                row.Cells.Add(totalCell);

                totalCell.Text = "Total";
                totalCell.ColumnSpan = 2;
                totalCell.HorizontalAlign = HorizontalAlign.Right;

                TableCell priceCell = new TableCell();
                priceCell.Text = shoppingCart.TotalAmountPayable.ToString();
                row.Controls.Add(priceCell);

                tblShoppingCart.Controls.Add(row);
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Bookstore.aspx");
    }

    protected void btnEmptyShoppingCart_Click(object sender, EventArgs e)
    {
        //Todo: Add your code here

        ShoppingCart shoppingCart = (ShoppingCart)Session["shoppingCart"];

        tblShoppingCart.Rows.Clear();
        shoppingCart.BookOrders.Clear();


        if (shoppingCart.IsEmpty)
        {
            TableHeaderRow rows = new TableHeaderRow();
            TableHeaderCell cells = new TableHeaderCell();

            cells.Text = "Title";
            rows.Cells.Add(cells);

            cells = new TableHeaderCell();
            cells.Text = "Quantity";
            rows.Controls.Add(cells);

            cells = new TableHeaderCell();
            cells.Text = "Subtotal";
            rows.Controls.Add(cells);

            tblShoppingCart.Controls.Add(rows);


            TableRow row = new TableRow();
            tblShoppingCart.Rows.Add(row);

            TableCell varningCell = new TableCell();
            row.Cells.Add(varningCell);

            varningCell.Text = "Your shopping cart is empty";
            varningCell.ForeColor = System.Drawing.Color.Red;
            varningCell.ColumnSpan = 3;
            varningCell.HorizontalAlign = HorizontalAlign.Center;

            tblShoppingCart.Controls.Add(row);
        }

    }

}