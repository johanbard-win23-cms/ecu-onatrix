# Umbraco CMS
- Pages, Elements, BlockGrid och BlockList med exempel på hur content manager kan hantera dessa
- css för BlockGrid-elements (u-* i _umbraco.scss [ecu-onatrix-html-css](https://github.com/johanbard-win23-cms/ecu-onatrix-html-css))
- surfaceController för hantering av formulär-data
- Flera olika lösningar för formulär-validering och hantering av formulär-data
  - Live JS-validering
  - Ren JS-lösning som validerar vid submit och skickar direkt till [contactProvider](https://github.com/johanbard-win23-cms/ecu-onatrix-contactProvider) via fetch
  - JS-lösning som validerar vid submit och skickar till surfaceController via fetch, surfaceController bollar vidare till [contactProvider](https://github.com/johanbard-win23-cms/ecu-onatrix-contactProvider)
  - Ren C#-lösning för formulär i Umbraco, validerar i surfaceController vid submit och skickar till [contactProvider](https://github.com/johanbard-win23-cms/ecu-onatrix-contactProvider)
