# Quick Introduction to Project

### Introduction

- Project has been configured to use Swagger so best & easiest testing can be achieved by it without obfuscating any other details about the requirements.

> ***NOTE*** Please read this entire document before testing the Project!

**Authentication**

- API-Key should be given before the HTTPPost method testing begins, ApiKeyValue is : 7a8a7cd837b042b58b56617114f4d3d7 (Also can be found on 'AppSetting.json' file).

**Test Cases ready JSONs**

**Case I) - Downloading Webpage then Converting to .pdf from external source**

- JSON body is given below, note how the property 'downloadableProperty' is set to true, since now the application will download and render then convert the HTML from the Url to the .PDF document.

{
  "downloadableProperty": true,
  "htmlString": "https://ironpdf.com/tutorials/html-to-pdf/",
  "options": {
    "colorMode": "Color",
    "pageOrientation": "Portrait",
    "pagePaperSize": "A4",
    "pageMargins": {
      "top": 10,
      "right": 10,
      "bottom": 10,
      "left": 10
    },
    "errorMessage": ""
  }
}


**Case II) - Taking parameter based HTML JSON-ified string & Converting to .pdf**

- JSON body is given below, note how the property 'downloadableProperty' is set to false now, since now the application can take the HTML from the parameter as JSON & then convert to the .PDF document.

{
  "downloadableProperty": false,
  "htmlString": "PGgxPkhlbGxvITwvaDE+\",\n    \"options\": {\n        \"pageColorMode\": \"Color\",\n        \"pageOrientation\": \"Portrait\",\n        \"pagePaperSize\": \"A4\",\n        \"pageMargins\": {\n            \"top\": 10,\n            \"right\": 10,\n            \"bottom\": 10,\n            \"left\": 10\n        }\n        \n    }\n}",
  "options": {
    "colorMode": "Color",
    "pageOrientation": "Portrait",
    "pagePaperSize": "A4",
    "pageMargins": {
      "top": 10,
      "right": 10,
      "bottom": 10,
      "left": 10
    },
    "errorMessage": ""
  }
}

-NOTE: Of course in case of bad validations the FluentValidator will notify of a failure & so will the failure of Authentication by providing the wrong ApiKey.
