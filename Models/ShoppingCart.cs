using System;
using System.Collections;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ShoppingCart
/// </summary>
public class ShoppingCart
{
    private ArrayList bookOrders;
    public ArrayList BookOrders { get { return bookOrders; } }

    public int NumOfItems { get { return bookOrders.Count; } }

    public bool IsEmpty { get { return bookOrders.Count == 0; } }

    public double TotalAmountPayable
    {
        get
        {
            double total = 0.0;
            foreach (BookOrder order in BookOrders)
            {
                total += order.Book.Price * order.NumOfCopies;
            }
            return total;
        }
    }
    
    public void AddBookOrder(BookOrder order)
    {
        bookOrders.Add(order);
    }
    public ShoppingCart()
    {
        this.bookOrders = new ArrayList();
    }

    public ShoppingCart(ArrayList bookOrders)
    {
        this.bookOrders = bookOrders;
    }

    public BookOrder findBookById(string elementId)
    {
        foreach(BookOrder book in BookOrders)
        {
            if (book.Book.Id == elementId)
            {
                return book;
            }
        }
        return null;
    }
}