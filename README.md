 Sopa de Letras

Sopa de Letras
==============

**Contenido**

*   [Introducción](#intro)
*   [Entorno de Desarrollo](#environment)
*   [Instalación](#install)
*   [Pruebas](#test)

Introducción
------------

El objetivo del presente proyecto es determinar si una palabra se encuentra o no en una sopa de letras.

### Reglas

*   Las palabras pueden estar en dirección vertical, horizontal o diagonal.
*   Las palabras pueden estar escritas de izquierda a derecha o en orden inverso, de derecha a izquierda.
*   Las palabras pueden estar escritas de abajo hacia arriba, o en orden inverso de arriba hacia abajo.
*   Las palabras pueden cambiar de dirección en cualquier sílaba, por ejemplo, la palabra susana, puede aparecer con la sílaba "su" en sentido vertical, la sílaba "sa" en horizontal y la sílaba "na" de nuevo en vertical.
*   Las palabras están acentuadas correctamente.
*   Las palabras pueden estar en mayúsculas o minúsculas, pero no mezcladas.

Los datos para el ejercicio son los siguientes:

`// Palabras que están en la sopa de letras ciertas = ['TIBURÓN', 'LEOPARDO', 'PUMA', 'COCODRILO', 'LEÓN', 'DELFÍN', 'TIGRE', 'ÁGUILA', 'LOBO', 'GUEPARDO']; // Palabras que no están en la sopa de letras falsas = ['ANGUILA', 'TIBURON', 'BUFALO']; // Sopa de letras sopa = [ ['J', 'V', 'Ó', 'R', 'U', 'B', 'I', '0', 'F', 'N', 'N', 'Y', 'A', 'T', 'F'], ['C', 'L', 'V', 'Ó', 'U', 'M', 'T', 'L', 'Y', 'L', 'B', 'H', 'D', 'W', 'J'], ['H', 'U', 'V', 'N', 'H', 'O', 'C', 'A', 'Y', 'O', 'B', 'D', 'Y', 'I', 'G'], ['C', 'O', 'V', 'T', 'R', 'D', 'B', 'Z', 'T', 'U', 'U', 'P', 'C', 'E', 'U'], ['E', 'X', 'A', 'C', 'A', 'G', 'H', 'R', 'G', 'F', 'P', 'E', 'O', 'H', 'E'], ['Q', 'X', 'Y', 'V', 'P', 'L', 'I', 'N', 'E', 'Y', 'X', 'V', 'C', 'W', 'P'], ['O', 'B', 'O', 'M', 'O', 'H', 'K', 'Ó', 'E', 'H', 'M', 'H', 'O', 'M', 'A'], ['J', 'X', 'L', 'P', 'E', 'T', 'D', 'E', 'Z', 'T', 'T', 'F', 'D', 'G', 'R'], ['D', 'E', 'T', 'N', 'L', 'W', 'W', 'L', 'S', 'A', 'I', 'G', 'R', 'W', 'D'], ['I', 'M', 'X', 'F', 'M', 'A', 'J', 'N', 'L', 'Y', 'G', 'B', 'I', 'O', 'O'], ['C', 'C', 'P', 'O', 'I', 'U', 'Y', 'I', 'Í', 'B', 'R', 'J', 'L', 'G', 'K'], ['O', 'R', 'Z', 'A', 'W', 'Z', 'U', 'T', 'I', 'F', 'E', 'L', 'O', 'T', 'G'], ['Q', 'A', 'M', 'U', 'P', 'G', 'D', 'O', 'R', 'K', 'L', 'C', 'I', 'V', 'N'], ['S', 'N', 'K', 'N', 'Á', 'Q', 'P', 'G', 'C', 'X', 'H', 'E', 'J', 'D', 'F'], ['Z', 'S', 'P', 'F', 'M', 'L', 'P', 'S', 'S', 'Z', 'T', 'K', 'D', 'L', 'G'] ];`  
  
  

Entorno de Desarrollo
---------------------

El software utilizado para el desarrollo de este proyecto es el siguiente:

*   Windows 10 Pro
*   Visual Studio Comunity 2019
*   node.js versión 12.18.1
*   Angular CLI: 12.2.8

  
  
  

Instalación
-----------

**NOTA: Para poder ejecutar el proyecto de manera local, es necesario tener instalado el software mencionado anteriormente.**

Descargue o clóne el repositorio desde [aquí](https://github.com/haliovilla/alphabet-soup.git)

`git clone https://github.com/haliovilla/alphabet-soup.git`

### Back-end

Haga doble clic sobre el archivo llamado "Alphabet-API.sln" para abrir la solución en Visual Studio y posteriormente pulse F5 para ejecutar el proyecto.  
  
Esto ejecutará la web API del proyecto, que es donde se realizan las búsquedas en la sopa de letras.

### Front-end

Navegue hasta la carpeta "alphabet-soup" e instale las dependencias necesarias.

`cd alphabet-soup npm install`

Una vez finalizada la instalación, ejecute el proyecto mediante el comando ng serve.

`ng serve`  
  
  

Pruebas
-------

Desde un navegador web, navegue hasta http://localhost:4200.

Al hacerlo se mostrará la sopa de letras predeterminada y, al lado derecho de la pantalla, la lista de las palabras que sehan buscado al cargar el sitio.

Para buscar una palabra en específico, diríjase a la parte inferior, busque el campo de texto marcado con la etiqueta "Palabra a buscar" y modifíquela.  
Para buscarla haga clic en el botón "Validar".  
El resultado se agregará a la lista de resultados.

Si desea modificar la sopa de letras, haga clic en el botón "Cambiar Vista" ubicado en la parte superior izquierda.  
En el campo de texto que aparecerá inserte la nueva sopa de letras y, a continuación, establezca el tamaño de la misma en el campo de texto marcado con la etiqueta "Tamaño de la Matríz".

**NOTA: Es importante que el número de filas sea igual que el número de letras por fila, haciendo así una matríz de N x N, en donde N es el número de letras por fila y el número de filas.**  
  

Para probar el proyecto en línea vaya a [este enlace](http://sopa.ti-plus.net).

Si desea usar la API, puede hacerlo mediante peticiones POST a [esta url](https://alphabet.ti-plus.net/api).

La API acepta el siguiente objeto:

`export interface AlphabetModel { AlphabetSoup: string[]; // la sopa de letras WordToFind: string; // la palabra a buscar SoupSize: number; // el tamaño de la matríz }`

Y devuelve el siguiente objeto:

`export interface ApiResult { Word: string; // la palabra buscada WordExists: boolean; // determina si la palabra se encuentra o no en la sopa de letras Direction: string; // determina la dirección en la que se encuentra la palabra dentro de la sopa de letras }`