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
using DevExpress.Web.Data;
using System.Collections;
using DevExpress.Web.ASPxGridView;

public partial class _Default : System.Web.UI.Page {
    protected List<Person> Persons {
        get {
            const string key = "DX1";
            if(Session[key] == null) {
                Session[key] = CreatePersons();
            }
            return (List<Person>)Session[key];
        }
    }
    protected List<Category> Categories {
        get {
            const string key = "DX2";
            if(Session[key] == null) {
                Session[key] = CreateCategories();
            }
            return (List<Category>)Session[key];
        }
    }

    protected override void OnInit(EventArgs e) {
        base.OnInit(e);
        Grid.DataSource = Persons;
        Grid.DataBind();
    }

    protected void Grid_RowUpdating(object sender, ASPxDataUpdatingEventArgs e) {
        Person person = FindPersonById((int)e.Keys[0]);
        person.Name = e.NewValues["Name"].ToString();

        CheckBoxList list = (CheckBoxList)Grid.FindEditRowCellTemplateControl((GridViewDataColumn)Grid.Columns[2], "List");        
        if(Grid.IsCallback)
            LoadListControlPostDataOnCallback(list);

        person.Categories.Clear();
        foreach(ListItem item in list.Items) {
            if(item.Selected)
                person.Categories.Add(FindCategoryByName(item.Value));
        }

        e.Cancel = true;
        Grid.CancelEdit();
    }

    protected Category FindCategoryByName(string categoryName) {
        foreach (Category item in Categories) {
		    if(item.Name == categoryName)
                return item;
	    }
        return null;
    }
    protected Person FindPersonById(int id) {
        foreach(Person item in Persons) {
            if(item.ID == id)
                return item;
        }
        return null;
    }

    protected List<Person> CreatePersons() {
        List<Person> persons = new List<Person>();
        persons.Add(new Person(1, "Alex", Categories[1], Categories[2]));
        persons.Add(new Person(2, "Bill", Categories[0]));
        persons.Add(new Person(3, "Kate", Categories[2]));
        return persons;
    }
    protected List<Category> CreateCategories() {
        List<Category> categories = new List<Category>();
        categories.Add(new Category("Family"));
        categories.Add(new Category("Friends"));
        categories.Add(new Category("Business"));
        return categories;
    }

    protected void List_DataBound(object sender, EventArgs e) {
        CheckBoxList list = (CheckBoxList)sender;
        GridViewEditItemTemplateContainer container = (GridViewEditItemTemplateContainer)list.Parent;
        IDictionary hash = CreatePersonCategoriesHash(container.Grid.GetRowValues(container.VisibleIndex, "CategoriesString").ToString());
        foreach(ListItem item in list.Items)
            item.Selected = hash.Contains(item.Value);
    }
    IDictionary CreatePersonCategoriesHash(string catString) {
        Hashtable table = new Hashtable();
        string[] names = catString.Split(new string[] { Person.CategorySeparator }, StringSplitOptions.None);
        foreach(string name in names)
            table.Add(name, null);
        return table;
    }

    // workaround for std ListControl LoadPostData
    void LoadListControlPostDataOnCallback(ListControl control) {
        if(!Grid.IsEditing) return;
        foreach(ListItem item in control.Items)
            item.Selected = false;
        foreach(string key in Request.Params.AllKeys) {
            IPostBackDataHandler dataHandler = control as IPostBackDataHandler;
            if(key.StartsWith(control.UniqueID))
                dataHandler.LoadPostData(key, Request.Params);
        }
    }
}
