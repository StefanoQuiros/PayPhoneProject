1. Levantar el proyecto WalletApiSolution.sln, es el que contiene todos los proyectos.
2. Seleccionar como proyecto de inicio "WalletApiPayphone" dentro de Visual Studio.
3. La base de datos esta hecha en SQL y el script para restaurarla en cualquier servidor SQL Server esta en la carpeta "SQL Database Script".
4. La cadena de conexión esta en appsettings.json como "DefaultConnection".

5. ¿Cómo tu implementación puede ser escalable a miles de
transacciones?

Hay varias formas de escalar a un flujo alto transaccional, voy a enumerar 2:

* La primera implementando un sistema de colas como Azure Service Bus para no hacer un cuello de botella a la hora de procesar las transacciones que llegan, sino que el "Queueing" se encarge de enviar las transacciones por medio de la cola.

* La otra forma podría ser la utilización de microservicios con autoescalacion o pago por uso como lo hacen Azure, AWS si hay un flujo muy alto transaccional se aumentan automáticamente las prestaciones para cumplir con las necesidades del momento.

4. ¿Cómo tu implementación asegura el principio de
idempotencia?

Al realizar el proyecto no le di énfasis a este punto de manera especifica, pero aun asi como buenas practicas se realizaron varios procesos para mitigar posibles problemas de este tipo como por ejemplo manejar un identity único para cada transacción, además si ocurriese un problema en el proceso de actualización o envió de transacciones, entity framework realiza una reversion del flujo ya sea crear, actualizar o algún proceso delicado, además a nivel de capa de presentación se pueden realizar mas validaciones para mitigar esto de manera conjunta entre el frontend y el backend así asegurar la idempotencia de la solución en general, definitivamente la solución se puede mejorar en este punto.

5. ¿Cómo protegerías tus servicios para evitar ataques de
Denegación de servicios, sql injection, CSRF?

* Para evitar ataques de DoS hay varias formas que se pueden implementar, por ejemplo limitar la cantidad de solicitudes que un mismo usuario pueda realizar en cierto lapso de tiempo, en temas de transacciones solamente usuarios logueados, en este caso se utilizo JWT para autenticacion pueden accesar a la mayoría de endpoints, no es muy usual que a nivel transaccional en una Wallet digital un solo usuario haga mas de mil transacciones en cierto tiempo por lo que se puede bloquear después de cierta cantidad de solicitudes, además en el proceso de login si un usuario realiza muchos intentos se puede bloquear por exceso de intentos al iniciar sesión.

* Para SQL Injection el uso de Entity Framework de manera nativa permite delimitar las consultas que se realizan a la base de datos, para otras formas de acceso a base de datos por medio de consultas y procedimientos almacenados se podría delimitar el acceso no permitiendo concatenación de parámetros directamente como un string, podría ayudar a evitar SQL Injection.

* Para CSRF con JWT se puede erradicar que se realice Cross-SiteRequest

6. ¿Cuál sería tu estrategia para migrar un monolito a
microservicios?

* Empezaria por entender el proyecto un análisis exhaustivo del mismo, identificar el objetivo de la migración, el alcance de la misma y posibles mejoras en la estructura base del proyecto a migrar, identificamos los procesos mas importantes del miso para comenzar a descomponer y migrar por prioridad, tamaño, primero los modulos mas críticos, luego ir por orden de prioridad a los que vayan siendo menos críticos y/o mas pequeños, la migración debe hacerse de manera gradual, dependiendo del tamaño del proyecto va a depender que tan desafiante sea la migración, automatización de todos y cada uno de los flujos con un proceso robusto de pruebas y un buen pipeline de CI/CD. Si se planifica bien, se estudia a profundidad el proyecto, se automatiza de manera adecuada y se realizan las pruebas necesarias se garantiza una migración limpia así aseguramos la continuidad del negocio. 


7. ¿Qué alternativas a la solución requerida propondrías para una
solución escalable?

Definitivamente microservicios con la infraestructura auto escalable de acuerdo a las necesidades y un sistema de colas.

