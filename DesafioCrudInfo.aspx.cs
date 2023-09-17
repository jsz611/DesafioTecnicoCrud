using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Configuration;

namespace DesafioTecnicoCrud
{
    public partial class DesafioCrudInfo : System.Web.UI.Page
    {
        private string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataAdapter adapter;
        private DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(cs);
            cmd = new SqlCommand();
            cmd.Connection = con;

            if (!IsPostBack)
            {
                LoadGridViewData();
            }
        }

        private void LoadGridViewData()
        {
            dt = new DataTable();
            adapter = new SqlDataAdapter("SELECT ProductID, NameItem, Price, DescriptionProduct, Quantity, RegistrationDate, UpdateDate FROM Products", con);
            adapter.Fill(dt);

            dgViewProducts.DataSource = dt;
            dgViewProducts.DataBind();
        }

        protected void dgViewProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgViewProducts.SelectedIndex >= 0)
            {
                LblSID.Text = dgViewProducts.SelectedRow.Cells[1].Text;
                TxtNameItem.Text = dgViewProducts.SelectedRow.Cells[2].Text;
                TxtPrice.Text = dgViewProducts.SelectedRow.Cells[3].Text;
                TxtDescriptionProduct.Text = dgViewProducts.SelectedRow.Cells[4].Text;
                TxtQuantity.Text = dgViewProducts.SelectedRow.Cells[5].Text;
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtNameItem.Text) && !string.IsNullOrEmpty(TxtPrice.Text) && !string.IsNullOrEmpty(TxtDescriptionProduct.Text) && !string.IsNullOrEmpty(TxtQuantity.Text))
            {
                decimal priceValue;
                int quantityValue;

                if (decimal.TryParse(TxtPrice.Text, out priceValue) && int.TryParse(TxtQuantity.Text, out quantityValue))
                {
                    if (!IsProductAlreadyExists(TxtNameItem.Text))
                    {
                        using (SqlConnection con = new SqlConnection(cs))
                        {
                            con.Open();
                            cmd = new SqlCommand("INSERT INTO Products (NameItem, Price, DescriptionProduct, Quantity, RegistrationDate) VALUES (@NameItem, @Price, @DescriptionProduct, @Quantity, @RegistrationDate)", con);
                            cmd.Parameters.AddWithValue("@NameItem", TxtNameItem.Text);
                            cmd.Parameters.AddWithValue("@Price", priceValue);
                            cmd.Parameters.AddWithValue("@DescriptionProduct", TxtDescriptionProduct.Text);
                            cmd.Parameters.AddWithValue("@Quantity", quantityValue);
                            cmd.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            LblMessage.Text = "Produto adicionado com sucesso!";

                            ClearFields();

                            LoadGridViewData();
                        }
                    }
                    else
                    {
                        LblMessage.Text = "O produto já existe na tabela.";
                    }
                }
                else
                {
                    LblMessage.Text = "O preço e a quantidade devem ser valores numéricos.";
                }
            }
            else
            {
                LblMessage.Text = "Preencha todas as informações";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LblSID.Text))
            {
                decimal priceValue;
                int quantityValue;

                if (decimal.TryParse(TxtPrice.Text, out priceValue) && int.TryParse(TxtQuantity.Text, out quantityValue))
                {
                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        con.Open();
                        cmd = new SqlCommand("SELECT COUNT(*) FROM Products WHERE NameItem = @NameItem AND ProductID <> @ProductID", con);
                        cmd.Parameters.AddWithValue("@NameItem", TxtNameItem.Text);
                        cmd.Parameters.AddWithValue("@ProductID", LblSID.Text);
                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            LblMessage.Text = "O novo valor já existe.";
                        }
                        else
                        {
                            cmd = new SqlCommand("SELECT * FROM Products WHERE ProductID = @ProductID AND (NameItem <> @NameItem OR Price <> @Price OR DescriptionProduct <> @DescriptionProduct OR Quantity <> @Quantity)", con);
                            cmd.Parameters.AddWithValue("@NameItem", TxtNameItem.Text);
                            cmd.Parameters.AddWithValue("@Price", priceValue);
                            cmd.Parameters.AddWithValue("@DescriptionProduct", TxtDescriptionProduct.Text);
                            cmd.Parameters.AddWithValue("@Quantity", quantityValue);
                            cmd.Parameters.AddWithValue("@ProductID", LblSID.Text);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    reader.Close();
                                    cmd = new SqlCommand("UPDATE Products SET NameItem = @NameItem, Price = @Price, DescriptionProduct = @DescriptionProduct, Quantity = @Quantity, UpdateDate = @UpdateDate WHERE ProductID = @ProductID", con);
                                    cmd.Parameters.AddWithValue("@NameItem", TxtNameItem.Text);
                                    cmd.Parameters.AddWithValue("@Price", priceValue);
                                    cmd.Parameters.AddWithValue("@DescriptionProduct", TxtDescriptionProduct.Text);
                                    cmd.Parameters.AddWithValue("@Quantity", quantityValue);
                                    cmd.Parameters.AddWithValue("@ProductID", LblSID.Text);
                                    cmd.Parameters.AddWithValue("@UpdateDate", DateTime.Now);
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                    LblMessage.Text = "Produto atualizado com sucesso!";

                                    ClearFields();

                                    LoadGridViewData();
                                }
                                else
                                {
                                    LblMessage.Text = "Nenhum valor foi alterado.";
                                }
                            }
                        }
                    }
                }
                else
                {
                    LblMessage.Text = "O preço e a quantidade devem ser valores numéricos.";
                }
            }
        }

        private bool IsProductAlreadyExists(string productName)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                cmd = new SqlCommand("SELECT COUNT(*) FROM Products WHERE NameItem = @NameItem", con);
                cmd.Parameters.AddWithValue("@NameItem", productName);
                int count = (int)cmd.ExecuteScalar();
                con.Close();
                return count > 0;
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LblSID.Text))
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    cmd = new SqlCommand("DELETE FROM Products WHERE ProductID = @ProductID", con);
                    cmd.Parameters.AddWithValue("@ProductID", LblSID.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    TxtNameItem.Text = string.Empty;
                    TxtPrice.Text = string.Empty;
                    TxtDescriptionProduct.Text = string.Empty;
                    TxtQuantity.Text = string.Empty;

                    LblMessage.Text = "Produto excluído com sucesso!";

                    LoadGridViewData();
                }
            }
            else
            {
                LblMessage.Text = "Selecione um produto para excluir.";
            }
        }

        protected void BtnCancelOperation_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            LblSID.Text = "";
            TxtNameItem.Text = "";
            TxtPrice.Text = "";
            TxtDescriptionProduct.Text = "";
            TxtQuantity.Text = "";
            LblMessage.Text = "";
        }
    }
}