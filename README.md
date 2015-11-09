# AdamExtensions
Project containing several extensions to the ADAM framework to make the life as ADAM partner a little easier

## How to deploy to Config studio (aka System)
* download sources and build them
* copy the built dll (CbxSw.AdamExtensions.dll) to the bin folder of Config studio
* open the studio's web.config and add the xml below in the /configuration/adam.web.studio section:
```xml
<studioExtensions>
  <providers>
	<add type="CbxSw.AdamExtensions.StudioStartup, CbxSw.AdamExtensions" />
  </providers>
</studioExtensions>
```
* you're set to go: just navigate to your ConfigStudio and type in {studioUrl}/FieldInheritances/{fieldName} (e.g.:http://localhost/System/FieldInheritances/myFieldName)
