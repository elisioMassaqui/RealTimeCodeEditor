# Resumo do Script CodeEditor

O script CodeEditor é parte de um projeto Unity que utiliza Roslyn para permitir a edição, compilação e execução dinâmica de código C# diretamente no jogo. Esse editor de código in-game facilita a criação e modificação de scripts em tempo real, proporcionando uma interação mais ágil e iterativa.

[!](https://github.com/elisioMassaqui/RealTimeCodeEditor/blob/master/Anota%C3%A7%C3%A3o%202024-06-11%20202519.png)

Componentes Principais:
Bibliotecas Utilizadas:

Unity:
UnityEngine, UnityEngine.UI, TextMeshPro (para elementos da interface do usuário).
Roslyn:
Microsoft.CodeAnalysis, Microsoft.CodeAnalysis.CSharp (para compilação de código C#).
Outras:
System.Reflection, System.Text, System.Linq, System.IO (para manipulação e compilação de código em tempo de execução).
Atributos Públicos:

TMP_InputField codeInputField: Campo de entrada de texto para o código.
Button compileButton, Button runAgainButton: Botões para compilar e executar o código novamente.
TextMeshProUGUI outputText: Campo de texto para exibir saídas ou mensagens de erro.
MyScript myScript: Referência a outro script MyScript.
Métodos e Funcionalidades:

Update: Chama instanciarBolinha do MyScript quando a tecla de espaço é pressionada.
exemplo: Inicializa myScript.
Awake: Inicializa o InputField com um código padrão e compila esse código.
Start: Configura os botões para chamar CompileAndRunCode e RunCompiledCode.
CompileAndRunCode: Compila e executa o código inserido no InputField, atualizando a interface com mensagens de erro ou sucesso.
RunCompiledCode: Executa o método Run do script compilado e atualiza a interface com a mensagem retornada.
CompileAssembly: Compila o código C# dinamicamente utilizando Roslyn e retorna um Assembly.
Código Padrão:

```csharp

private string defaultCode = @"
using UnityEngine;

public class DynamicScript : MonoBehaviour
{
    public string message;

    public void Run()
    {
        message = ""Hello, Universe!"";

        MyScript myScript = FindObjectOfType<MyScript>();
        if (myScript != null)
        {
            myScript.myVariable = ""World!"";
            myScript.instanciarBolinha();
        }
        else
        {
            Debug.LogError(""MyScript não encontrado na cena!"");
        }
    }

    public string GetMessage()
    {
        return message;
    }
}";
```

Este código padrão fornece um exemplo básico de script que pode ser modificado e executado pelo usuário.

Integração com Roslyn:
O método CompileAssembly utiliza Roslyn para compilar o código C# dinamicamente. Ele cria uma árvore de sintaxe (SyntaxTree) a partir do código fornecido e adiciona as referências necessárias, incluindo as específicas do Unity. A compilação é realizada em memória, e se bem-sucedida, um Assembly é carregado para execução.

Uso:
Inicialização:

O campo de entrada de texto (codeInputField) é preenchido com um código padrão ao iniciar.
A compilação inicial é realizada durante o Awake.
Interação:

O usuário pode editar o código no campo de entrada e clicar em "Compile" para compilar e executar o código.
O botão "Run Again" permite a reexecução do código compilado sem necessidade de recompilação, útil para testes rápidos.
Este editor de código in-game oferece uma maneira poderosa e flexível para desenvolver e testar scripts diretamente no ambiente Unity, acelerando o processo de desenvolvimento e permitindo iterações rápidas.
