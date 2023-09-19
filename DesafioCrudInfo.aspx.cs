using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using DesafioTecnicoCrud.Models;

namespace DesafioTecnicoCrud
{
    public partial class DesafioCrudInfo : System.Web.UI.Page
    {
        private List<Product> productList;

        protected void Page_Load(object sender, EventArgs e)
        {
            productList = Session["ProductList"] as List<Product>;

            if (productList == null)
            {
                productList = new List<Product>();
                Session["ProductList"] = productList;
            }

            if (!IsPostBack)
            {
                LoadGridViewData();
            }
        }

        private void LoadGridViewData()
        {
            dgViewProducts.DataSource = productList;
            dgViewProducts.DataBind();
        }

        protected void dgViewProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedProductID = Convert.ToInt32(dgViewProducts.DataKeys[dgViewProducts.SelectedIndex].Value);

            Product selectedProduct = productList.FirstOrDefault(p => p.ProductID == selectedProductID);

            if (selectedProduct != null)
            {
                LblSID.Text = selectedProduct.ProductID.ToString();
                TxtNameItem.Text = selectedProduct.NameItem;
                TxtPrice.Text = selectedProduct.Price.ToString();
                TxtDescriptionProduct.Text = selectedProduct.DescriptionProduct;
                TxtQuantity.Text = selectedProduct.Quantity.ToString();

                LblMessage.Text = "";
            }
        }


        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            decimal priceValue;
            int quantityValue;
            if (string.IsNullOrWhiteSpace(TxtNameItem.Text) ||
            string.IsNullOrWhiteSpace(TxtPrice.Text) ||
            string.IsNullOrWhiteSpace(TxtDescriptionProduct.Text) ||
            string.IsNullOrWhiteSpace(TxtQuantity.Text))
            {
                LblMessage.Text = "Preencha todos os campos antes de adicionar um produto.";
                return; 
            }


            if (decimal.TryParse(TxtPrice.Text, out priceValue) && int.TryParse(TxtQuantity.Text, out quantityValue))
            {
                
                if (IsProductAlreadyExists(TxtNameItem.Text))
                {
                    LblMessage.Text = "Um produto com o mesmo nome já existe.";
                }
                else
                {
                    
                    Product newProduct = new Product
                    {
                        ProductID = GenerateUniqueProductID(),
                        NameItem = TxtNameItem.Text,
                        Price = priceValue,
                        DescriptionProduct = TxtDescriptionProduct.Text,
                        Quantity = quantityValue,
                        RegistrationDate = DateTime.Now,
                        UpdateDate = null 
                    };

                    
                    productList.Add(newProduct);

                    LblMessage.Text = "Produto adicionado com sucesso!";

                    LoadGridViewData();

                    
                    ClearFields();
                }
            }
            else
            {
                LblMessage.Text = "O preço e a quantidade devem ser valores numéricos.";
            }
        }

        private bool IsProductAlreadyExists(string productName)
        {
            if (productName == null)
            {
                return false; 
            }

            foreach (var product in productList)
            {
                if (product.NameItem != null && product.NameItem.Equals(productName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false; 
        }


        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LblSID.Text))
            {
                decimal priceValue;
                int quantityValue;

                if (decimal.TryParse(TxtPrice.Text, out priceValue) && int.TryParse(TxtQuantity.Text, out quantityValue))
                {
                    int productIDToUpdate = int.Parse(LblSID.Text);

                    Product productToUpdate = productList.FirstOrDefault(p => p.ProductID == productIDToUpdate);

                    if (productToUpdate != null)
                    {
                        
                        if (productToUpdate.NameItem != TxtNameItem.Text || productToUpdate.Price != priceValue ||
                            productToUpdate.DescriptionProduct != TxtDescriptionProduct.Text ||
                            productToUpdate.Quantity != quantityValue)
                        {
                           
                            productToUpdate.NameItem = TxtNameItem.Text;
                            productToUpdate.Price = priceValue;
                            productToUpdate.DescriptionProduct = TxtDescriptionProduct.Text;
                            productToUpdate.Quantity = quantityValue;
                            productToUpdate.UpdateDate = DateTime.Now;

                            
                            LblMessage.Text = "Produto atualizado com sucesso!";

                      
                            LoadGridViewData();
                        }
                        else
                        {
                            LblMessage.Text = "Nenhum valor foi alterado.";
                        }
                    }
                    else
                    {
                        LblMessage.Text = "Produto não encontrado na lista.";
                    }
                }
                else
                {
                    LblMessage.Text = "O preço e a quantidade devem ser valores numéricos.";
                }
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LblSID.Text))
            {
                Product productToDelete = productList.FirstOrDefault(p => p.ProductID == int.Parse(LblSID.Text));

                if (productToDelete != null)
                {
                    productList.Remove(productToDelete);

                    ClearFields();

                    LoadGridViewData();

                    LblMessage.Text = "Produto excluído com sucesso!";
                }
                else
                {
                    LblMessage.Text = "Produto não encontrado na lista.";
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

        private int GenerateUniqueProductID()
        {
            return productList.Count > 0 ? productList.Max(p => p.ProductID) + 1 : 1;
        }
    }
}


namespace DesafioTecnicoCrud.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string NameItem { get; set; }
        public decimal Price { get; set; }
        public string DescriptionProduct { get; set; }
        public int Quantity { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? UpdateDate { get; set; } 
    }
}
