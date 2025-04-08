## Integrantes
- [Thor de Oliveira Morciani - G96DFD3](https://github.com/ThorMorciani)
- [Caio Henrique Barbosa Santos - G976690](https://github.com/CaioHennrique)
- [Daniel Fagundes da Mota Filho - G97BJA0](https://github.com/DANFAGUNDES0)
- [Murilo Câmara da Silva – G9843G0](https://github.com/MuriloCSilva)
- [Joel Rodrigues Pereira - R0507H0](https://github.com/JoelRP00)
- [João Pedro Costa Vieira - G956HJ7](https://github.com/JoaoPcvv)

## --- User Stories ---

## User Story: Abrir Chamado
Definição:
1. Logar no sistema de gerenciamento de chamados
2. Inserir informações sobre o chamado
3. Sugestões de solução automática da IA ( caso tenha )
4. Realizar o chamado
5. Sistema gera número de protocolo e salva o chamado
6. Caso a IA não resolva, encaminhar a um técnico especializado ( caso a IA 
encontre uma solução eficaz, chamado pode ser encerrado imediatamente pelo funcionário )

## User Story: Sugestões de solução automatizadas
Definição:
1. funcionário registra chamado
2. IA consulta o banco de dados e históricos de chamado
3. Caso haja uma solução compatível, a IA retorna a mesma solução.
4. Funcionário testa a solução sugerida
5. Caso resolva o problema, o chamado já pode ser encerrado.

## User Story: Encaminhamento de chamado ao Técnico adequado
Definição:
1. A IA analisa a descrição e categoria do chamado
2. Com base no histórico de atendimentos o sistema seleciona o técnico com a especialização mais adequada
3. Chamado chega ao técnico via notificação
4. Técnico assume o chamado e inicia a resolução

## User Story: Acompanhamento do status do chamado
Definição:
1. Funcionário acessa o sistema
2. Consulta a lista de chamados abertos
3. Visualiza os status (Aberto, Em andamento, Resolvida, Fechado)
4. Em caso de atualização, recebe uma notificação.

## User Story: Atualizar e fechar chamado
Definição:
1. o técnico acessa o sistema
2. visualiza os chamados que foram enviados a ele
3. Atualiza o status do atendimento realizado
4. Registra a solução
5. Define o chamado como Resolvido
6. Funcionário recebe notificação para validar a resolução
7. Se a solução foi eficiente, chamado pode ser fechado

## User Story: Gerar relatórios
Definição:
1. adm acessa o sistema
2. acessa o painel de administração
3. seleciona o período e critérios desejado ( tempo médio de atendimento, chamados resolvidos pela IA,
Ténicos mais eficientes, Tipos de problemas mais recorrentes)
4. possibilidade de gerar um relatório para analise.


## User Story: Verificar chamados abertos pelo funcionário
Definição:
1. funcionario acessa o sistema
2. clica na opção de chamados abertos
3. opção de filtros para status de chamados e/ou por protocolo

## User Story: Cadastrar Usuário
Definição:
1. Usuário faz login
2. Usuário insere as informações de cadastro do usuário
3. Ocorre a conferência dos dados para cadastro
4. Usuário é cadastrado

## --- FIM User Stories ---

## --- Backlog ---

1. Criar tabelas no banco de dados
2. Criar tela de Login
3. Criar tela de Listagem de Chamados
4. Criar fluxo de Cadastro de Chamados
5. Criar tela de Visualização de Chamados
6. Criar tela de Cadastro de Usuários
7. Criar tela de Listagem de Usuários
8. Criar tela de Cadastro de Criticidades
9. Criar tela de Listagem de Criticidades
10. Criar tela de Cadastro de Status
11. Criar tela de Listagem de Status
11. Criar tela de Visualização de Logs
12. Criar tela de Cadastro de Causas Raiz
13. Criar tela de Listagem de Causas Raiz
15. Criar tela de Cadastro de Perfis
16. Criar tela de Listagem de Perfis
17. Criar tela de Cadastro de Funcionalidades
18. Criar tela de Listagem de Funcionalidades
19. Criar serviço de CRUD para Usuários
20. Criar serviço de Login
21. Criar serviço de CRUD de Funcionalidades
22. Criar serviço de CRUD de Perfis
23. Criar serviço de CRUD de Criticidades
24. Criar serviço de CRUD de Causas Raiz
25. Criar serviço de CRUD de Status
26. Criar serviço de inserção de Logs
27. Criar serviço de CRUD de Chamados

## --- FIM Backlog ---

## --- Requisitos Funcionais ---

1. O sistema deverá possuir um gerenciamento de usuários com perfis e permissões.
2. O sistema deverá utilizar um módulo de IA para sugerir soluções rápidas para os chamados.
3. Deverão existir métodos de extração de relatórios do histórico de chamados e usuários.
4. O sistema deve permitir que um funcionário possa atender mais de um chamado ao mesmo tempo.
5. Uma notificação deverá ser enviada toda vez que um chamado for atribuído à um usuário.
6. O requerente receberá uma notificação toda vez que seu chamado tiver alguma atualização.
7. O sistema não deve permitir que chamados sejam abertos por meio da plataforma mobile.
8. O cadastro de novos usuários e extração de relatórios deverão ser feitos somente por usuários que possuem permissão.
9. O requerente não poderá alterar as informações do chamada após a abertura do mesmo.
10. Todo chamado deverá ser armazenado em um banco de dados para fins de histórico.

## --- FIM Requisitos Funcionais ---

## --- Requisitos Não Funcionais ---

1. O código deve seguir boas práticas de desenvolvimento para facilitar futuras manutenções.
2. O sistema deve permitir fácil atualização de modelos de IA sem necessidade de interrupção do serviço.
3. Documentação técnica e de usuário deve estar sempre atualizada.
4. Deve ser compatível com múltiplos navegadores modernos (Chrome, Edge, Firefox, Safari).
5. A interface deve ser responsiva e acessível em dispositivos móveis e desktops.
6. O tempo de resposta da categorização automática não deve ultrapassar 5 segundos.

## --- FIM Requisitos Não Funcionais ---