# AdamExtensions
Project containing several extensions to the ADAM framework to make the life as ADAM partner a little easier

## How to deploy to a studio
* copy the built dll (CbxSw.AdamExtensions.dll) to the bin folder of a studio
* open the studio's web.config and add the xml below in the /configuration/adam.web.studio section:
```xml
<studioExtensions>
  <providers>
	<add type="CbxSw.AdamExtensions.StudioStartup, CbxSw.AdamExtensions" />
  </providers>
</studioExtensions>
```

## How to configure
* [FieldInheritanceWidget](docs/FieldInheritanceWidget.md)
