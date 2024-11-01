# CIDA-DotNet8

## Integrantes
- Cauã Alencar Rojas Romero - RM98638
- Jaci Teixeira Santos – RM99627
- Leonardo dos Santos Guerra - RM99738
- Maria Eduarda Ferreira da Mata - RM99004

## Como rodar
- Coloque suas credencias da oracle e azure no arquivo appsettings.json e/ou use as que estão no final do readme
- Execute update database no terminal de gerenciamento de pacotes do NuGet
- Inicie a aplicação em http


## Escolha da Arquitetura para o Projeto da API em .NET
No nosso projeto de API em .NET, optamos por utilizar a arquitetura monolítica. Comparada com a arquitetura de microserviços, a arquitetura monolítica se destaca em aspectos como desempenho, implementação e desenvolvimento.

## Desempenho e Implementação
A arquitetura monolítica oferece um desempenho superior devido à ausência de latência entre as camadas da API, uma vez que todas as operações são realizadas em uma única base de código. Além disso, a implementação é facilitada pelo fato de o projeto ser único, simplificando o processo. O desenvolvimento também é menos complexo em uma API monolítica, pois não é necessário criar e gerenciar múltiplas aplicações separadas, como ocorre com a arquitetura de microserviços.


### Diferenças Principais entre Arquitetura Monolítica e Microserviços

1. **Complexidade e Estrutura:**
   - **Monolítica:** Toda a lógica da aplicação está integrada em uma única base de código, o que facilita o desenvolvimento e a gestão inicial.
   - **Microserviços:** A aplicação é dividida em serviços independentes que se comunicam entre si, aumentando a complexidade do sistema.

2. **Escalabilidade:**
   - **Monolítica:** Escala-se a aplicação inteira como um único bloco, o que pode ser ineficiente se apenas uma parte da aplicação estiver sob carga intensa.
   - **Microserviços:** Permite a escalabilidade individual de cada serviço conforme necessário, oferecendo uma escalabilidade mais granular.

3. **Desdobramento e Manutenção:**
   - **Monolítica:** Requer a reimplantação de toda a aplicação para qualquer alteração, o que, embora mais simples, é menos flexível.
   - **Microserviços:** Permite deploys e atualizações independentes de cada serviço, facilitando a manutenção e a inovação sem impactar toda a aplicação.

4. **Comunicação e Desempenho:**
   - **Monolítica:** A comunicação entre componentes é direta e interna, proporcionando melhor desempenho para aplicações menores.
   - **Microserviços:** A comunicação ocorre através de chamadas de rede entre serviços, o que pode introduzir latência e complexidade adicional.

## Design Patterns

1. **Dependency Injection**
2. **Builder Pattern**
3. **Factory Method Pattern**
4. **Adapter Pattern**
5. **Repository Pattern**
6. **Chain of Responsibility Pattern**
7. **Singleton Pattern**

## Testes
Nossa abordagem foi abrangente, focando tanto nos endpoints da API quanto em seus serviços. Implementamos testes que verificam não apenas o funcionamento isolado de cada componente, mas também sua integração com outros elementos do sistema. Os testes dos endpoints garantem o correto funcionamento das respostas HTTP e validam os payloads das requisições, enquanto os testes dos serviços verificam a lógica de negócio e os fluxos de dados.


## Práticas de Clean Code e SOLID
Aplicamos princípios de Clean Code de forma consistente ao longo do desenvolvimento. Isso inclui a definição de variáveis próximas ao seu ponto de uso, a separação adequada de código repetido em funções reutilizáveis e a escolha cuidadosa de nomes significativos para funções e variáveis. Os princípios SOLID foram fundamentais em nossa abordagem, com ênfase especial na separação de responsabilidades através do Single Responsibility Principle e na utilização efetiva de interfaces para promover um baixo acoplamento entre os componentes do sistema.

## Funcionalidades de IA Generativa
Na implementação das funcionalidades de IA, adotamos uma abordagem híbrida utilizando diferentes modelos e tecnologias para atender a necessidades específicas do sistema. Para o cálculo de preços de insights, desenvolvemos um algoritmo de deep learning baseado em regressão linear utilizando o framework ML.NET. Esta implementação permite uma precificação dinâmica e precisa.

Para a funcionalidade de geração de resumos, integramos o modelo phi3 mini, que se mostrou eficiente na condensação de informações mantendo os pontos principais do conteúdo original. Já para a geração de insights mais complexos e contextualizados, implementamos uma integração com o modelo gemini 1.5 flash, que oferece capacidades avançadas de processamento de linguagem natural e geração de conteúdo. A escolha destes modelos específicos foi baseada em um equilíbrio entre performance, precisão e requisitos computacionais, permitindo que nossa API ofereça resultados de alta qualidade mantendo tempos de resposta adequados

## Credenciais

````json
"ConnectionStrings": {
"FiapOracleConnection": "Data Source=oracle.fiap.com.br:1521/orcl;User ID=XXXXX;Password=XXXXX;",
"AzureStorage": "DefaultEndpointsProtocol=https;AccountName=XXXXX;AccountKey=XXXXX;EndpointSuffix=core.windows.net",
"AzureAIApiKey": "",
"AzureAIEndpoint": "",
"GeminiApiKey": ""
}
````
