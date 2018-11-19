# viafirma-documents-wpf-sample

Esta solución pretende mostrar de forma básica la integración de un cliente con tecnología c# con el CRM viafirma documents. 
Para ello, se realiza el siguiente flujo:

  - Enviar un documento PDF al CRM
  - Consultar el estado de la petición
  - Rechazar la petición de firma
  - Descargar el documento firmado una vez concluido el proceso por parte del usuario en su dispositivo

### Configuración

Para el correcto funcionamiento del ejemplo, es necesario configurar las siguientes constantes en el fichero [MainWindow.xaml.cs]: 

| Constante | Descripción |
| ------ | ------ |
| OAuthConsumerKey | Parámetro de credencial de integración *|
| OAuthConsumerSecret | Parámetro de credencial de integración * |
| userCode | Código de usuario al que se le enviará el documento |
| deviceCode | Código del dispositivo del usuario definido en la constante anterior al que se enviará la petición |
| appCodeDevice | Credencial de la  aplicación móvil al que se enviará la petición |
| urlApiBackend | Endpoint del servicio de viafirma documents a emplear en la solución (Por ejemplo: [https://sandbox.viafirma.com/documents/api/v3/]) |
| policyCode | Código de la plantilla del que extraer las políticas |


*Las credenciales gestionadas por viafirma documents para la integración de servicios esta compuestas por dos datos:
- OAuth Consumer Key
- OAuth Consumer Secret


   [MainWindow.xaml.cs]: <https://github.com/viavansi/viafirma-documents-wpf-sample/blob/master/ViafirmaDocumentsWpfAppSample/MainWindow.xaml.cs>
