# Unity_Json
Este projeto feito na Unity trata-se de um exemplo de como utilizar a Unity para conectar a serviços PHP, carregar dados de um arquivo JSON e exibir na UI.

Para usar o projeto você pode utilizar os arquivos de apoio que estão na pasta Assets do projeto.

Os arquivos de apoio devem ser descompactados em uma pasta pública do seu servidor local (se utilizar o XAMPP, criar uma pasta para inserir os arquivos dentro de htdocs)

O formato esperado do json é:
[{"apelido":"valor","pontos":"valor", "data","valor"}, {...}]

Após o projeto WEB ter criado o arquivo data.json, atualize, no script DataManager.cs, as variáveis url_send_data para o endereço localhost onde está o arquivo atualizar.php; Atualize a variável url_load_data para o endereço localhost onde está o arquivo recuperar.php.

Carregue a cena Main no projeto e, ao dar o "Play", você poderá ser capaz de cadastrar novos apelidos e recupeara os dados.
