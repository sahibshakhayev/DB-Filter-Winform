using LINQ_for_Entities;

namespace DBFilter
{
    public partial class Form1 : Form
    {
        private string[] criteria = new string[] { "Authors", "Categories", "Themes" };


        private LibraryContext context;
        public Form1()
        {
            InitializeComponent();
            context = new LibraryContext();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = criteria;
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            comboBox2.DataSource = null;
            listBox1.DataSource = null;


            switch (comboBox1.SelectedItem.ToString())
            {
                case "Authors":

                    var authors = context.Authors.Select(a => a.FirstName + " " + a.LastName);

                    comboBox2.DataSource = authors.ToList();
                    break;
                case "Categories":

                    var categories = context.Categories.Select(c => c.Name);

                    comboBox2.DataSource = categories.ToList();
                    break;
                case "Themes":

                    var themes = context.Themes.Select(t => t.Name);

                    comboBox2.DataSource = themes.ToList();
                    break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            listBox1.DataSource = null;

            
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Authors":
                   
                    var booksByAuthor = (from b in context.Books
                                        join a in context.Authors on b.IdAuthor equals a.Id
                                        where a.FirstName + " " + a.LastName == comboBox2.Text
                                        select b.Name).ToList();

                    listBox1.DataSource = booksByAuthor;
                    break;
                case "Categories":

                    var booksByCategory = (from b in context.Books
                                           join c in context.Categories on b.IdCategory equals c.Id
                                           where c.Name == comboBox2.Text
                                           select b.Name).ToList();
                    listBox1.DataSource = booksByCategory;
                    break;
                case "Themes":
                    var booksByTheme = (from b in context.Books
                                        join t in context.Themes on b.IdThemes equals t.Id
                                        where t.Name == comboBox2.Text
                                        select b.Name).ToList();

                    listBox1.DataSource = booksByTheme;
                    break;
            }
        }
    }
}