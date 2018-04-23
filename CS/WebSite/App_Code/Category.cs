using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public class Category {
    string name;

    public Category(string name) {
        this.name = name;
    }

    public string Name { get { return name; } }
}
