package com.tits.tits_mobile;

import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.squareup.picasso.Picasso;
import com.tits.tits_mobile.HttpHandler.HttpGetRequest;
import com.tits.tits_mobile.models.Ingredient;
import com.tits.tits_mobile.models.Product;

import java.util.ArrayList;
import java.util.concurrent.ExecutionException;

import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

public class EanActivity extends AppCompatActivity {

    TextView brand;
    TextView productName;
    TextView description;

    Button showIngredients;
    ImageView prodImg;
    String EAN = "";
    String pattern = "\\d{13}|\\d{8}";
    Product prod;
    ArrayList<Ingredient> ingList;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_ean);

        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);

        brand = findViewById(R.id.brand);
        productName = findViewById(R.id.productName);
        description = findViewById(R.id.description);
        showIngredients = findViewById(R.id.showIngredients);
        prodImg = findViewById(R.id.img);

        EAN = getIntent().getStringExtra("EAN");

        if (EAN != null && EAN.matches(pattern)) {
            EAN = getIntent().getStringExtra("EAN");

            //String myUrl = "http://217.182.79.249/api/products/getByEan?ean=" + EAN;   //String to place our result in
            String myUrl = "http://217.182.79.249/api/products/getFullProductInfo?gtin=" + EAN;
            final String result;


            HttpGetRequest getRequest = new HttpGetRequest(EanActivity.this);
            try {
                result = getRequest.execute(myUrl).get();

                if(result.equals("not found")){
                    AlertDialog.Builder builder = new AlertDialog.Builder(EanActivity.this);
                    builder.setMessage("Product not found, do you want to add new one?")
                            .setPositiveButton("Yes", new DialogInterface.OnClickListener() {
                                public void onClick(DialogInterface dialog, int id) {
                                    startActivity(new Intent(EanActivity.this, newProductForm.class).putExtra("EAN", EAN));
                                }
                            })
                            .setNegativeButton("No", new DialogInterface.OnClickListener() {
                                public void onClick(DialogInterface dialog, int id) {

                                }
                            });
                    // Create the AlertDialog object and return it
                    AlertDialog alertDialog = builder.create();
                    alertDialog.show();


                } else {
                    prod = new ObjectMapper().configure(DeserializationFeature.FAIL_ON_UNKNOWN_PROPERTIES, false).readValue(result, Product.class);
                    if(prod.getIngredients() == null) {
                        AlertDialog.Builder builder1 = new AlertDialog.Builder(EanActivity.this);
                        builder1.setMessage("Product found, but no ingredients in our DB's, do you want add?")
                                .setPositiveButton("Yes", new DialogInterface.OnClickListener() {
                                    public void onClick(DialogInterface dialog, int id) {
                                        startActivity(new Intent(EanActivity.this, productWithoutIndgredients.class));
                                    }
                                })
                                .setNegativeButton("No", new DialogInterface.OnClickListener() {
                                    public void onClick(DialogInterface dialog, int id) {
                                        Picasso.get().load(prod.getProductImage()).into(prodImg);
                                        brand.setText(prod.getBrand());
                                        productName.setText(prod.getProductName());
                                        description.setText(prod.getDescription());
                                    }
                                });
                        // Create the AlertDialog object and return it
                        AlertDialog alertDialog1 = builder1.create();
                        alertDialog1.show();
                    } else {
                        Picasso.get().load(prod.getProductImage()).into(prodImg);
                        brand.setText(prod.getBrand());
                        productName.setText(prod.getProductName());
                        description.setText(prod.getDescription());

                        ingList = prod.getIngredients();

                    }
                }
                //eanTxtView.setText(prod.getProductName());
            } catch (ExecutionException e) {
                e.printStackTrace();
            } catch (InterruptedException e) {
                e.printStackTrace();
            } catch (JsonMappingException e) {
                e.printStackTrace();
            } catch (JsonProcessingException e) {
                e.printStackTrace();
            }
        } else {
            brand.setText("Invalid EAN");
        }

        showIngredients.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Bundle extra = new Bundle();
                extra.putSerializable("arr", ingList);
                startActivity(new Intent(EanActivity.this, ingredientsList.class).putExtra("ingList", extra));
            }
        });

    }

}
