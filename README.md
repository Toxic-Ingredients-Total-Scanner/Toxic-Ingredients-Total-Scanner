# Toxic-Ingredients-Total-Scanner

The project was created as part of the Database course at the Poznan University of Technology.

## Overview
Main goal of the project was to create the app which returns to the user list of ingredients (based on EAN code - scanned or typed by the user) that the product has. <br/>
Application creates separate requests for every ingredient to external database getting info like harmfulness, molecular formulas or chemical particle visualization.
The source of these information is [PubChem](https://pubchem.ncbi.nlm.nih.gov/). <br/>
All of ingredients are processed with requests to the translate API to unify the language of results. <br/>
User can use the application using web or android application.
<br/>
Access to the product database was granted by [GS1 Polska - Produkty w sieci](http://produktywsieci.gs1.pl/).


## Features
- Scanning EAN-8 and EAN-13 barcodes
- Displaying hints when typing product name
- Displaying information about existing product
- User has the possibility to edit the product or to add the new one if database doesn’t contains it
- User has access to information if any of the ingredients are harmful 

## Database schema

<img src="https://i.imgur.com/tKgdoxw.png" style="width: 500px"/>

## Application requests flow schema 

<img src="https://i.imgur.com/ktFEnbP.png" style="width: 700px"/>

## Performance tests (VPS load when inserting >50 000 records sequentially - CPU 10-50%, RAM ~5%)

<img src="https://i.imgur.com/cIuLDAk.png" style="width: 500px"/>

## Contribution
Repository: https://github.com/Toxic-Ingredients-Total-Scanner/Toxic-Ingredients-Total-Scanner <br/>
Issue tracker: https://github.com/Toxic-Ingredients-Total-Scanner/Toxic-Ingredients-Total-Scanner/issues 

## License
MIT

## Credits
### Authors
- Michał Ratajczak 
- Konrad Tarnacki
- Kacper Wleklak 
- Mateusz Galan 
- Norbert Pałuczyński 
### Access to the products database granted by: 
[GS1 Polska - Produkty w sieci](http://produktywsieci.gs1.pl/)
