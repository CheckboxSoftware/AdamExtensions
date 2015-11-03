# Add the widget to the configured studio widgets
* Go to System studio
* In the System studio's menu, select Guides - Widgets
* In the left section: select the studio where you want to install the widget
* In the "Registered widgets in {studio} studio" section, click on the xml snippet next to System value
* On the bottom left hand side: click Source editor
* Append this xml:
```xml
  <add name="FieldInheritanceWidget" type="CbxSw.AdamExtensions.Products.FieldInheritance.FieldInheritanceWidget, CbxSw.AdamExtensions" />
```
* Click save

# Put the widget on the home page of a studio
Assuming you have ran through the steps above
* Go to System studio
* In the System studio's menu, select Guides - Widgets
* In the left section: select the studio where you want to configure the widget
* In the "Configure the {studio} studio homepage" section, click on the xml snippet next to System value
* Click "Add widget"
* From the dropdown select "FieldInheritanceWidget"
* Set the location and the size of the widget
* Click save
