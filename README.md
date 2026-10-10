# Ludumdare — Pizza Panic

Jogo 2D feito para a Ludum Dare 34, em Unity (6000.3.9f1). Você é um entregador que equilibra pizzas na bandeja e as entrega nos baús dos motoboys.

## Projeto

Há um único projeto Unity, `ludumdare34_mobile/`, que roda em desktop e em celular:

- **Desktop:** setas ou A/D para mover; menus e botões com o mouse.
- **Mobile:** toque na metade esquerda ou direita da tela.
- `ludumdare34_mobile/Packages/com.playnow.pizzapanic.core/` — pacote embutido no projeto. Contém os sistemas que criam a própria UI por código, sem depender de edição das cenas: HUD de combo, tremor de câmera e partículas, pausa/volume/mudo, tutorial, curva de dificuldade e pizzas especiais.

## Como jogar

- Pegue as pizzas na bandeja e encoste no baú do motoboy para entregar.
- Entregas seguidas dentro de 1 segundo formam **combo** (a barra no topo mostra o tempo restante).
- Pizza **dourada** vale x5, pizza **verde** devolve uma vida, pizza **queimada** tira pontos e quebra o combo.
- Quanto mais pizzas na bandeja, mais lento você fica.
- A dificuldade sobe com o tempo e com o número de entregas.
- `Esc` ou `P` (ou o botão **II**) pausa o jogo e abre volume/mudo. O tutorial aparece só na primeira partida.

## Abrindo no Unity

Abra a pasta `ludumdare34_mobile/` com o Unity 6000.3.9f1. Para gerar o jogo de desktop, selecione a plataforma **Windows/Mac/Linux** em Build Profiles; para celular, **Android** ou **iOS**. A mesma base serve para todas.

- Já que no github não permite salvar pastas em branco, adicionei um shell script caso seja necessário ter algum(s) diretório(s) em branco no repositório. Só lembrar de rodar o `create-gitkeep.sh` antes de mandar as alterações.


- Ps: caso Alguém não conheça o como usar o git, só me falar que eu colocarei os passos básicos aqui!



# Passos para configuração do ambiente usanod git

- 1. Criar a sua conta no github : `https://github.com/`
- 2. Baixar o Git Bash `https://git-scm.com/downloads` 
- 3. No git bash, usar o comando `ssh-keygen` para gerar a chave.
- 3.1 Usar o comando `cat ~/.ssh/id_rsa.pub` para ler a chave ou alternativamente abrir o arquivo e copiar a chave manualmente.
- 4  Em `Settings -> SSH Keys` addicionar a chave gerada anteriormente.
- 5. No git bash e na pasta onde será a workspace do jogo,
- 6. Crie um fork do projeto principal `https://github.com/johnnytn/ludumdare`
- 7. Clonar o seu fork `git clone git@github.com:XX/ludumdare.git`
- 8. Adicionar o alias pro para o projeto principal `git remote add upstream git@github.com:johnnytn/ludumdare.git`


A partir daqui o workspace estará configurado, agora é só começar!

# Trabalhando com o Git

Após adicionar o repositório, para toda nova alteraração que for realizada e será enviada pro github no final é precisará fazer 3 passos: 
- 1. git add arquivo ou *(adiciona tudo) ou *.extensão -> adiciona os arquivos que serão enviados.
- 2, git commit -m" mensagem " -> realiza o commit e escreve a mensagem dele.
- 3, git push -> envia as alterações.

Ps: Sempre lembrar de dar `git fetch` e `git rebase` antes de realizar qualquer push para o master, para sempre ter o master sincornizado e sem conflitos.




