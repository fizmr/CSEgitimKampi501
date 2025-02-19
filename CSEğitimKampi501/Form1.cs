using CSEğitimKampi501.Dtos;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSEğitimKampi501
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Server=FURKAN\\SQLEXPRESS;initial Catalog = EgitimKampi501DB;integrated security = true");
        private async void btnList_Click(object sender, EventArgs e)
        {
            string query = "Select* from tblProduct";
            var values = await connection.QueryAsync<ResultProductDto>(query);
            dataGridView1.DataSource = values;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            string query = "Insert into tblProduct (ProductName,ProductPrice,ProductStock,ProductCategory) values (@Name,@Price,@Stock,@Category)";
            var parameters = new DynamicParameters();
            parameters.Add("@Name", txtName.Text);
            parameters.Add("@Price", txtPrice.Text);
            parameters.Add("@Stock", txtStock.Text);
            parameters.Add("@Category", txtCategory.Text);
            await connection.ExecuteAsync(query, parameters);
            MessageBox.Show("Ürün Başarıyla Eklendi!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            string query = "Delete from tblProduct where ProductId = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", txtID.Text);
            await connection.ExecuteAsync(query, parameters);
            MessageBox.Show("Ürün Başarıyla Silindi!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            string query = "Update tblProduct set ProductName = @Name, ProductPrice = @Price, ProductStock = @Stock, ProductCategory = @Category where ProductId = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Name", txtName.Text);
            parameters.Add("@Price", txtPrice.Text);
            parameters.Add("@Stock", txtStock.Text);
            parameters.Add("@Category", txtCategory.Text);
            parameters.Add("@Id", txtID.Text);
            await connection.ExecuteAsync(query, parameters);
            MessageBox.Show("Ürün Başarıyla Güncellendi!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string query = "Select* from tblProduct";
            var productTotalCount = await connection.QueryAsync(query);
            txtTotalProduct.Text = productTotalCount.Count().ToString();

            string query2 = "Select ProductName from TblProduct where ProductPrice=(Select MAX(ProductPrice) from TblProduct)";
            var productMaxPrice = await connection.QueryAsync(query2);
            txtMaxPriceProduct.Text = productMaxPrice.FirstOrDefault().ProductName;

            string query3 = "Select Distinct ProductCategory from TblProduct";
            var productCategoryCount = await connection.QueryAsync(query3);
            txtTotalCategory.Text = productCategoryCount.Count().ToString();

        }
    }
}
