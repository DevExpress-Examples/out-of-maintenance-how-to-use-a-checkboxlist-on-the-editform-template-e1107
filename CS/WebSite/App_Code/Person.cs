using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;

public class Person {
    public const string CategorySeparator = ", ";

    int id;
    string name;
    List<Category> categories;

    public Person(int id, string name, params Category[] categories) {
        this.id = id;
        this.name = name;
        this.categories = new List<Category>(categories);
    }

    public int ID { get { return id; } }
    public string Name {
        get { return name; }
        set { name = value; }
    }
    public List<Category> Categories { get { return categories; } }
    public string CategoriesString {
        get {
            StringBuilder builder = new StringBuilder();
            foreach(Category item in Categories) {
                if(builder.Length > 0)
                    builder.Append(CategorySeparator);
                builder.Append(item.Name);
            }
            return builder.ToString();
        } 
    }
}
