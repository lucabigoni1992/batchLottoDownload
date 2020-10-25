--installare angular 8

npm install @angular/cli@8

npm install --save @progress/kendo-ui-angular-allpackage 
--save @progress/kendo-angular-grid @progress/kendo-angular-dropdowns @progress/kendo-angular-inputs @progress/kendo-angular-dateinputs @progress/kendo-data-query @progress/kendo-angular-intl @progress/kendo-angular-l10n @progress/kendo-drawing @progress/kendo-angular-excel-export @progress/kendo-angular-buttons @progress/kendo-angular-dialog @progress/kendo-angular-gauges @progress/kendo-angular-charts @angular/animations
npm install @angular/material@8

npm install @angular/cdk@8

///installazione di progetto andare nella cartella del sito contenete il file pakage.json nel quale vi sono le librerie angular da installare e usare il comando npm install

///problemi

Problema dovuto a chiamate che fallisnono i ragestry potrebbero essere errati
C:\Users\lbigoni>npm config list
	; cli configs
	metrics-registry = "http://tfs-web:8080/source/_packaging/Apparound@Local/npm/registry/"
	scope = ""
	user-agent = "npm/6.14.8 node/v12.19.0 win32 x64"

	; userconfig C:\Users\lbigoni\.npmrc
	always-auth = true
	registry = "http://tfs-web:8080/source/_packaging/Apparound@Local/npm/registry/"

	; builtin config undefined
	prefix = "C:\\Users\\lbigoni\\AppData\\Roaming\\npm"

	; node bin location = C:\Program Files\nodejs\node.exe
	; cwd = C:\Users\lbigoni
	; HOME = C:\Users\lbigoni
	; "npm config ls -l" to show all defaults.
	


npm config set registry "http://tfs-web:8080/source/_packaging/Apparound@Local/npm/registry/"
npm config set metrics-registry  "http://tfs-web:8080/source/_packaging/Apparound@Local/npm/registry/"
npm config set strict-ssl false





npm config set registry "http://registry.npmjs.org"
npm config set metrics-registry  "http://registry.npmjs.org"
npm config set strict-ssl false