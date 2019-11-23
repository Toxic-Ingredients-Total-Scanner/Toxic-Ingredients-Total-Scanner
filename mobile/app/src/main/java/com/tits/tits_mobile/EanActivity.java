package com.tits.tits_mobile;

import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.FileNotFoundException;
import java.util.concurrent.ExecutionException;

import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

public class EanActivity extends AppCompatActivity {

    TextView eanTxtView;
    Button anotherScan;
    String EAN = "";
    String pattern = "\\d{13}|\\d{8}";


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_ean);

        eanTxtView = findViewById(R.id.eanTxtView);
        anotherScan = findViewById(R.id.anotherScan);

        EAN = getIntent().getStringExtra("EAN");

        if (EAN != null && EAN.matches(pattern)) {
            EAN = getIntent().getStringExtra("EAN");

            String myUrl = "http://217.182.79.249/api/products/getByEan?ean=" + EAN;   //String to place our result in
            final String result;

            JSONObject json;


            HttpGetRequest getRequest = new HttpGetRequest();
            try {
                result = getRequest.execute(myUrl).get();

                System.out.println(result);
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
                    json = new JSONObject(result);
                }
                eanTxtView.setText(result);
            } catch (ExecutionException e) {
                e.printStackTrace();
            } catch (InterruptedException e) {
                e.printStackTrace();
            } catch (JSONException e) {
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
