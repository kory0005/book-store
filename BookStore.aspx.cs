using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookStore : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //TODO: Add you code here 

        ///// create list of books /////
        ShoppingCart shoppingCart = null;

        //Check if the session is empty
        if (Session["shoppingCart"] != null)
        {
            //Read actual value from session
            shoppingCart = (ShoppingCart)Session["shoppingCart"];
        }
        else
        {
            shoppingCart = new ShoppingCart();
            //Use the value from list of students
            Session["shoppingCart"] = shoppingCart;
        }

        lblNumItems.Text = shoppingCart.NumOfItems + " item(s)";

        ///// dynamically creating a dropdown list /////
        if (!IsPostBack)
        {
            foreach (Book bookItem in BookCatalogDataAccess.GetAllBooks())
            {
                if (shoppingCart.findBookById(bookItem.Id) == null)
                {
                    drpBookSelection.Items.Add(new ListItem(bookItem.Title, Convert.ToString(bookItem.Id)));
                }
            }

        }
    }
    protected void drpBookSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        //TODO: Add your code here

        ///// show selected book description and price/////
        foreach (ListItem item in drpBookSelection.Items)
        {
            if (item.Selected == true)
            {
                Book selectedBook = BookCatalogDataAccess.GetBookById(item.Value);

                lblPrice.Text = "$" + selectedBook.Price.ToString();
                lblDescription.Text = selectedBook.Description;
            }

        }
    }

    protected void btnAddToCart_Click(object sender, EventArgs e)
    {
        //TODO: Add your code here
        if (!Page.IsValid) return;

        ShoppingCart shoppingCart = (ShoppingCart)Session["shoppingCart"];

        foreach (ListItem item in drpBookSelection.Items)
        {
            if (item.Selected == true)
            {
                Book selectedBook = BookCatalogDataAccess.GetBookById(item.Value);

                lblDescription.Text = txtQuantity.Text + " copy(s) of " + selectedBook.Title + " is added to the shopping cart";

                drpBookSelection.SelectedValue = "-1";

                int copies = Convert.ToInt16(txtQuantity.Text);

                shoppingCart.AddBookOrder(new BookOrder(selectedBook, copies));

                lblNumItems.Text = shoppingCart.NumOfItems + " item(s)";

                drpBookSelection.Items.Remove(item);
                break;
            }
        }
    }
    protected void btnViewCart_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShoppingCartView.aspx");
    }
}