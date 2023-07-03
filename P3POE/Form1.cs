using RecipeApp2;

namespace P3POE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            foreach (Ingredients ingredients in ingredients)
            {
                lstDisplay.Items.Add(ingredients.RecipeName +
                    "/n" + ingredients.IngredientName +
                    "/n" + ingredients.Quantity +
                    "/n" + ingredients.Unit +
                    "/n" + ingredients.Calories +
                    "/n" + "Recipe Steps" +
                    "/n" + ingredients.Steps);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
         
        }
    }
}