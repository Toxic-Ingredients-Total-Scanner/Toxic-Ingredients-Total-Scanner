package com.tits.tits_mobile;

import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.tits.tits_mobile.HttpHandler.HttpGetRequest;
import com.tits.tits_mobile.models.Ingredient;
import com.tits.tits_mobile.models.Product;

import java.util.concurrent.ExecutionException;

import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

public class EanActivity extends AppCompatActivity {

    TextView eanTxtView;
    Button anotherScan;
    String EAN = "";
    String pattern = "\\d{13}|\\d{8}";
    Product prod;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_ean);

        eanTxtView = findViewById(R.id.eanTxtView);
        anotherScan = findViewById(R.id.anotherScan);

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
                        builder1.setMessage("Product found, but no ingtedients in our DB's, do you want add?")
                                .setPositiveButton("Yes", new DialogInterface.OnClickListener() {
                                    public void onClick(DialogInterface dialog, int id) {
                                        startActivity(new Intent(EanActivity.this, productWithoutIndgredients.class));
                                    }
                                })
                                .setNegativeButton("No", new DialogInterface.OnClickListener() {
                                    public void onClick(DialogInterface dialog, int id) {
                                        eanTxtView.setText(prod.toString());
                                    }
                                });
                        // Create the AlertDialog object and return it
                        AlertDialog alertDialog1 = builder1.create();
                        alertDialog1.show();
                    } else {
                        eanTxtView.setText(prod.toString());
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
            eanTxtView.setText("Invalid EAN");
        }

        anotherScan.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                    startActivity(new Intent(EanActivity.this, MainActivity.class));
            }
        });

    }

}
