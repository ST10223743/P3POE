using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RecipeApp2
{
    class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public double Calories { get; set; }
        public string FoodGroup { get; set; }
    }

    class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }

        public Recipe()
        {
            Ingredients = new List<Ingredient>();
            Steps = new List<string>();
        }

        public void EnterRecipe()
        {
            using (var form = new RecipeForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Name = form.RecipeName;

                    Ingredients.Clear();
                    foreach (var ingredientInfo in form.Ingredients)
                    {
                        Ingredient ingredient = new Ingredient();
                        ingredient.Name = ingredientInfo.Name;
                        ingredient.Quantity = ingredientInfo.Quantity;
                        ingredient.Unit = ingredientInfo.Unit;
                        ingredient.Calories = ingredientInfo.Calories;
                        ingredient.FoodGroup = ingredientInfo.FoodGroup;
                        Ingredients.Add(ingredient);
                    }

                    Steps.Clear();
                    Steps.AddRange(form.Steps);
                }
            }
        }

        public void DisplayRecipe()
        {
            using (var form = new DisplayRecipeForm(this))
            {
                form.ShowDialog();
            }
        }

        public double CalculateTotalCalories()
        {
            double totalCalories = Ingredients.Sum(ingredient => ingredient.Calories * ingredient.Quantity);
            return totalCalories;
        }
    }

    class RecipeForm : Form
    {
        private TextBox recipeNameTextBox;
        private Button addIngredientButton;
        private Button addStepButton;
        private Button saveButton;
        private ListBox ingredientListBox;
        private ListBox stepListBox;
        private List<Ingredient> ingredients;
        private List<string> steps;

        public RecipeForm()
        {
            InitializeComponent();
            ingredients = new List<Ingredient>();
            steps = new List<string>();
        }

        private void InitializeComponent()
        {
            recipeNameTextBox = new TextBox();
            addIngredientButton = new Button();
            addStepButton = new Button();
            saveButton = new Button();
            ingredientListBox = new ListBox();
            stepListBox = new ListBox();
            SuspendLayout();

            // Recipe Name TextBox
            recipeNameTextBox.Location = new System.Drawing.Point(12, 12);
            recipeNameTextBox.Name = "recipeNameTextBox";
            recipeNameTextBox.Size = new System.Drawing.Size(250, 20);
            recipeNameTextBox.TabIndex = 0;
            recipeNameTextBox.Text = "Enter the recipe name";

            // Add Ingredient Button
            addIngredientButton.Location = new System.Drawing.Point(12, 40);
            addIngredientButton.Name = "addIngredientButton";
            addIngredientButton.Size = new System.Drawing.Size(120, 23);
            addIngredientButton.TabIndex = 1;
            addIngredientButton.Text = "Add Ingredient";
            addIngredientButton.UseVisualStyleBackColor = true;
            addIngredientButton.Click += AddIngredientButton_Click;

            // Add Step Button
            addStepButton.Location = new System.Drawing.Point(142, 40);
            addStepButton.Name = "addStepButton";
            addStepButton.Size = new System.Drawing.Size(120, 23);
            addStepButton.TabIndex = 2;
            addStepButton.Text = "Add Step";
            addStepButton.UseVisualStyleBackColor = true;
            addStepButton.Click += AddStepButton_Click;

            // Save Button
            saveButton.Location = new System.Drawing.Point(12, 69);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(250, 23);
            saveButton.TabIndex = 3;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += SaveButton_Click;

            // Ingredient ListBox
            ingredientListBox.FormattingEnabled = true;
            ingredientListBox.Location = new System.Drawing.Point(12, 98);
            ingredientListBox.Name = "ingredientListBox";
            ingredientListBox.Size = new System.Drawing.Size(250, 134);
            ingredientListBox.TabIndex = 4;

            // Step ListBox
            stepListBox.FormattingEnabled = true;
            stepListBox.Location = new System.Drawing.Point(12, 238);
            stepListBox.Name = "stepListBox";
            stepListBox.Size = new System.Drawing.Size(250, 134);
            stepListBox.TabIndex = 5;

            // RecipeForm
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(274, 387);
            Controls.Add(recipeNameTextBox);
            Controls.Add(addIngredientButton);
            Controls.Add(addStepButton);
            Controls.Add(saveButton);
            Controls.Add(ingredientListBox);
            Controls.Add(stepListBox);
            Name = "RecipeForm";
            Text = "Enter Recipe";

            ResumeLayout(false);
            PerformLayout();
        }

        private void AddIngredientButton_Click(object sender, EventArgs e)
        {
            using (var ingredientForm = new IngredientForm())
            {
                if (ingredientForm.ShowDialog() == DialogResult.OK)
                {
                    Ingredient ingredient = new Ingredient();
                    ingredient.Name = ingredientForm.IngredientName;
                    ingredient.Quantity = ingredientForm.Quantity;
                    ingredient.Unit = ingredientForm.Unit;
                    ingredient.Calories = ingredientForm.Calories;
                    ingredient.FoodGroup = ingredientForm.FoodGroup;
                    ingredients.Add(ingredient);
                    ingredientListBox.Items.Add($"{ingredient.Name}, {ingredient.Quantity} {ingredient.Unit}");
                }
            }
        }

        private void AddStepButton_Click(object sender, EventArgs e)
        {
            using (var stepForm = new StepForm())
            {
                if (stepForm.ShowDialog() == DialogResult.OK)
                {
                    string step = stepForm.Step;
                    steps.Add(step);
                    stepListBox.Items.Add(step);
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            RecipeName = recipeNameTextBox.Text;
            Ingredients = ingredients;
            Steps = steps;
            Close();
        }

        public string RecipeName { get; private set; }
        public List<Ingredient> Ingredients { get; private set; }
        public List<string> Steps { get; private set; }
    }

    class IngredientForm : Form
    {
        private TextBox ingredientNameTextBox;
        private TextBox quantityTextBox;
        private TextBox unitTextBox;
        private TextBox caloriesTextBox;
        private TextBox foodGroupTextBox;
        private Button saveButton;

        public IngredientForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            ingredientNameTextBox = new TextBox();
            quantityTextBox = new TextBox();
            unitTextBox = new TextBox();
            caloriesTextBox = new TextBox();
            foodGroupTextBox = new TextBox();
            saveButton = new Button();
            SuspendLayout();

            // Ingredient Name TextBox
            ingredientNameTextBox.Location = new System.Drawing.Point(12, 12);
            ingredientNameTextBox.Name = "ingredientNameTextBox";
            ingredientNameTextBox.Size = new System.Drawing.Size(250, 20);
            ingredientNameTextBox.TabIndex = 0;
            ingredientNameTextBox.Text = "Enter the ingredient name";

            // Quantity TextBox
            quantityTextBox.Location = new System.Drawing.Point(12, 38);
            quantityTextBox.Name = "quantityTextBox";
            quantityTextBox.Size = new System.Drawing.Size(250, 20);
            quantityTextBox.TabIndex = 1;
            quantityTextBox.Text = "Enter the quantity";

            // Unit TextBox
            unitTextBox.Location = new System.Drawing.Point(12, 64);
            unitTextBox.Name = "unitTextBox";
            unitTextBox.Size = new System.Drawing.Size(250, 20);
            unitTextBox.TabIndex = 2;
            unitTextBox.Text = "Enter the unit of measurement";

            // Calories TextBox
            caloriesTextBox.Location = new System.Drawing.Point(12, 90);
            caloriesTextBox.Name = "caloriesTextBox";
            caloriesTextBox.Size = new System.Drawing.Size(250, 20);
            caloriesTextBox.TabIndex = 3;
            caloriesTextBox.Text = "Enter the calories";

            // Food Group TextBox
            foodGroupTextBox.Location = new System.Drawing.Point(12, 116);
            foodGroupTextBox.Name = "foodGroupTextBox";
            foodGroupTextBox.Size = new System.Drawing.Size(250, 20);
            foodGroupTextBox.TabIndex = 4;
            foodGroupTextBox.Text = "Enter the food group";

            // Save Button
            saveButton.Location = new System.Drawing.Point(12, 142);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(250, 23);
            saveButton.TabIndex = 5;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += SaveButton_Click;

            // IngredientForm
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(274, 177);
            Controls.Add(ingredientNameTextBox);
            Controls.Add(quantityTextBox);
            Controls.Add(unitTextBox);
            Controls.Add(caloriesTextBox);
            Controls.Add(foodGroupTextBox);
            Controls.Add(saveButton);
            Name = "IngredientForm";
            Text = "Add Ingredient";

            ResumeLayout(false);
            PerformLayout();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            IngredientName = ingredientNameTextBox.Text;
            Quantity = double.Parse(quantityTextBox.Text);
            Unit = unitTextBox.Text;
            Calories = double.Parse(caloriesTextBox.Text);
            FoodGroup = foodGroupTextBox.Text;
            Close();
        }

        public string IngredientName { get; private set; }
        public double Quantity { get; private set; }
        public string Unit { get; private set; }
        public double Calories { get; private set; }
        public string FoodGroup { get; private set; }
    }

    class StepForm : Form
    {
        private TextBox stepTextBox;
        private Button saveButton;

        public StepForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            stepTextBox = new TextBox();
            saveButton = new Button();
            SuspendLayout();

            // Step TextBox
            stepTextBox.Location = new System.Drawing.Point(12, 12);
            stepTextBox.Multiline = true;
            stepTextBox.Name = "stepTextBox";
            stepTextBox.Size = new System.Drawing.Size(250, 100);
            stepTextBox.TabIndex = 0;
            stepTextBox.Text = "Enter the step";

            // Save Button
            saveButton.Location = new System.Drawing.Point(12, 118);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(250, 23);
            saveButton.TabIndex = 1;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += SaveButton_Click;

            // StepForm
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(274, 153);
            Controls.Add(stepTextBox);
            Controls.Add(saveButton);
            Name = "StepForm";
            Text = "Add Step";

            ResumeLayout(false);
            PerformLayout();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Step = stepTextBox.Text;
            Close();
        }

        public string Step { get; private set; }
    }

    class DisplayRecipeForm : Form
    {
        private TextBox recipeTextBox;
        private Button closeButton;

        public DisplayRecipeForm(Recipe recipe)
        {
            InitializeComponent();
            recipeTextBox.Text = GetRecipeText(recipe);
        }

        private void InitializeComponent()
        {
            recipeTextBox = new TextBox();
            closeButton = new Button();
            SuspendLayout();

            // Recipe TextBox
            recipeTextBox.Location = new System.Drawing.Point(12, 12);
            recipeTextBox.Multiline = true;
            recipeTextBox.Name = "recipeTextBox";
            recipeTextBox.ReadOnly = true;
            recipeTextBox.ScrollBars = ScrollBars.Vertical;
            recipeTextBox.Size = new System.Drawing.Size(400, 400);
            recipeTextBox.TabIndex = 0;

            // Close Button
            closeButton.Location = new System.Drawing.Point(12, 418);
            closeButton.Name = "closeButton";
            closeButton.Size = new System.Drawing.Size(75, 23);
            closeButton.TabIndex = 1;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += CloseButton_Click;

            // DisplayRecipeForm
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(424, 453);
            Controls.Add(recipeTextBox);
            Controls.Add(closeButton);
            Name = "DisplayRecipeForm";
            Text = "Display Recipe";

            ResumeLayout(false);
            PerformLayout();
        }

        private string GetRecipeText(Recipe recipe)
        {
            string recipeText = $"Recipe:" +$" {recipe.Name} Ingredients: ";
            foreach (var ingredient in recipe.Ingredients)
            {
                recipeText += $"{ingredient.Name}: {ingredient.Quantity} {ingredient.Unit} ";
            }

            recipeText += $" Steps: ";
            for (int i = 0; i < recipe.Steps.Count; i++)
            {
                recipeText += $"{i + 1}. {recipe.Steps[i]} ";
            }

            recipeText += $" Total Calories: {recipe.CalculateTotalCalories()}";
            return recipeText;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    class Program
    {
        static void Main()
        {
            Recipe recipe = new Recipe();
            recipe.EnterRecipe();
            recipe.DisplayRecipe();
        }
    }
}
