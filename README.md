# Projeto_Hospital
Sistema Hospitalar com o intuito de cadastrar pacientes e verificar se estão com Covid-19 e tomar uma ação de acordo com a situação.

# Funcionalidades
- Chamar próximo paciente (Busca se paciente já possui cadastro, se tiver coloca em uma fila, senão solicita dados para cadastro)
- Chamar paciente para exame (realiza triagem de filas para cada dois chamados da fila preferencial, chama umda da fila normal)
  -   Coleta dados do resultado do teste de Covid-19
  -   Coleta quantidade em dias com sintomas
  -   Coleta dados dos sintomas
  -   Coleta dados se possuir comorbidades (no máximo cinco)
  -   Solicita qual ação tomar de acordo com as informações solicitadas ao paciente
      -   Dar Alta (Implementado) [Salva no histórico (em arquivo) e libera o paciente]
      -   Colocar em quarentena (Implementado) [Salva no histórico (em arquivo) e libera o paciente para ficar em quarentena]
      -   Mandar para emergência (Não Implementado)
- Localizar paciente na fila 
- Buscar por histórico do paciente
- Visualizar pacientes aguardando na fila normal
- Visualizar pacientes aguardando na fila preferencial
