# TESTES ARQUIVOS

## Funcionalidades Testadas

1. **PostArquivo_ReturnsArquivo**  
2. **PutArquivo_ReturnsArquivo_WhenArquivoExists**  
3. **DeleteArquivo_ReturnsNoContent_WhenArquivoExists**  
4. **GetArquivoByEmail_ReturnsArquivo_WhenArquivoExists**  
5. **GetArquivoById_ReturnsArquivo_WhenArquivoExists**  
6. **GetArquivoByIdUsuario_ReturnsArquivo_WhenArquivoExists**  
7. **GetArquivosSearch_ReturnsArquivo_WhenArquivoExists**  
8. **PostArquivo_ReturnsBadRequest_WhenSendRandomJson**

---

## Explicação e Importância de Cada Teste

### 1. PostArquivo_ReturnsArquivo
- **Objetivo:** Verificar se o upload de um arquivo é realizado corretamente.
- **Importância:** Este teste assegura que a API consegue receber e processar um arquivo enviado via `POST`. Isso é fundamental para garantir o fluxo de upload de documentos, uma funcionalidade essencial na aplicação.
- **Verificação:** Certifica que o status de resposta é `201 Created` e que o arquivo criado não é nulo, garantindo que o processo de upload foi bem-sucedido.

### 2. PutArquivo_ReturnsArquivo_WhenArquivoExists
- **Objetivo:** Verificar se é possível atualizar um arquivo existente.
- **Importância:** Esse teste garante que a funcionalidade de atualização de arquivos funciona corretamente, permitindo modificações nos dados de arquivos existentes.
- **Verificação:** O teste espera um status de sucesso e que os dados retornados estejam corretos, comparando o arquivo atualizado com os dados enviados.

### 3. DeleteArquivo_ReturnsNoContent_WhenArquivoExists
- **Objetivo:** Verificar se a API permite deletar um arquivo existente.
- **Importância:** A exclusão de arquivos é uma funcionalidade essencial. Este teste garante que, ao deletar um arquivo, o status de sucesso `204 No Content` é retornado, validando que a remoção foi concluída com sucesso.
- **Verificação:** O status `204 No Content` é esperado para confirmar que o arquivo foi deletado.

### 4. GetArquivoByEmail_ReturnsArquivo_WhenArquivoExists
- **Objetivo:** Verificar se a API retorna arquivos associados a um e-mail específico.
- **Importância:** Esse teste valida a capacidade da API de buscar arquivos com base em um critério específico (e-mail), o que pode ser essencial para a pesquisa de documentos.
- **Verificação:** O teste verifica se a resposta contém os arquivos correspondentes, validando que o sistema de busca por e-mail está funcional.

### 5. GetArquivoById_ReturnsArquivo_WhenArquivoExists
- **Objetivo:** Verificar se a API retorna um arquivo específico pelo seu ID.
- **Importância:** Garante que a funcionalidade de busca por ID está funcionando corretamente, permitindo a recuperação de arquivos específicos de maneira direta.
- **Verificação:** O teste espera que o arquivo retornado não seja nulo, confirmando que o arquivo foi encontrado e recuperado com sucesso.

### 6. GetArquivoByIdUsuario_ReturnsArquivo_WhenArquivoExists
- **Objetivo:** Verificar se a API retorna arquivos associados a um ID de usuário.
- **Importância:** A pesquisa por arquivos relacionados a um usuário é uma funcionalidade central em muitos sistemas de armazenamento. Este teste valida a integridade dessa operação.
- **Verificação:** O teste assegura que a resposta contém os arquivos associados ao usuário, garantindo a correção dessa funcionalidade.

### 7. GetArquivosSearch_ReturnsArquivo_WhenArquivoExists
- **Objetivo:** Verificar se a API retorna arquivos com base em uma pesquisa genérica.
- **Importância:** Permite testar a funcionalidade de busca por arquivos sem critérios específicos, garantindo que a pesquisa genérica retorna resultados esperados.
- **Verificação:** O teste valida se a resposta contém arquivos, confirmando que a busca está operando corretamente.

### 8. PostArquivo_ReturnsBadRequest_WhenSendRandomJson
- **Objetivo:** Verificar se a API retorna um erro ao receber um payload inválido no upload de arquivos.
- **Importância:** Esse teste garante que a API está validando adequadamente os dados recebidos e retorna o status correto (`415 Unsupported Media Type`) quando um formato inadequado é enviado.
- **Verificação:** A validação do retorno de erro é crucial para evitar que a API processe entradas inválidas, melhorando a segurança e robustez da aplicação.
