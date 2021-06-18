# Mercadolibre-ADN Challenge 

En esta seccion se documenta la solucion planteada al reto.


# Arquitectura planteada en la nube de AWS

Se usara docker en conjunto con elastic container service (ecs) para hostear el servicio, ecs ofrece la posibilidad de escalar horizontalmente y con esto soportar las fluctuaciones de alto consumo. Esto se complementa usando un aplicacion load balancer que puede distribuir la carga entre los nodos de manera eficiente.

Para guardar los resultados de las peticiones de validacion de mutaciones, se hace uso de Aws DynamoDB, esto porque es una base de datos clave valor para alto performance que a su vez es facil y sencilla de utilizar.En la siguiente imagen encuentre un diagrama donde se evidencia la arquitectura usada.

![Diagrama sin título](https://user-images.githubusercontent.com/32229478/122485536-d0cee580-cf9c-11eb-97af-5855e922c366.png)

# Automatizacion de pruebas unitarias con sonarcloud y pipelines de azure devops.

En la raiz del proyecto se encuentra un archivo llamado azure-pipelines.yml, el cual contiene los steps para ser ejecutados en un pipeline de azure devops, el cual se conecta con sonar cloud para verificar el code coverage, bugs, code smells, etc. Cada vez que se hace un pull request hacia master o development, este pipeline se ejecutara, y se puede evidenciar el estado de este en la seccion de Checks del pr.

A continuacion una imagen donde se ilustra la integracion.

![Diagrama sin título-Página-2](https://user-images.githubusercontent.com/32229478/122486230-4091a000-cf9e-11eb-9129-e7d44b00e68a.png)

En el siguiente enlace puede accder a ver los resultados: https://sonarcloud.io/dashboard?id=mercadolibre-dna

# Consumo de la api

La url para consumir el api es la siguiente:

http://mercadolibre-dna-lb-1742491657.us-east-1.elb.amazonaws.com/api/v1.0/dna/


Notese que el api esta versionada pensando en la escalabilidad.

Acontinuacion encuentre la coleccion en postman para mas facilidad con los dos endpoints /mutant y /stats

[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/b785258a82339ed75d5b)

# Como ejecutar el proyecto

1 Localizarse en la raiz del proyecto 

2 Construir la imagen docker, para eso puede ejecutar el siguiente comando:

docker build --no-cache -f MercadoLibre.Mutant.Dna.Api\Dockerfile -t mercadolibre-dna .

3 Correr un contendor de la imagen, para esto puede usar el puerto 80 de la maquina, ejecute el siguiente comando:

docker run -p 80:80 -e AWS_ACCESS_KEY_ID=XXXXXXXXXXXX -e AWS_SECRET_ACCESS_KEY=XXXXXXXXXXXXXXXXX -e AWS_DEFAULT_REGION=US-EAST-1 mercadolibre-dna:latest

Se pasan las variables de entorno de AWS_ACCESS_KEY_ID y AWS_SECRET_ACCESS_KEY para que el contenedor se pueda conectar a aws dynamodb de forma local, sin embargo, estas no son necesarias en el ambiente de aws cuando ya esta desplegado el componente. Estas llaves se enviaran via correo a la reclutadora.



















