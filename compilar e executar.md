using UnityEngine;
using UnityEngine.UI;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Reflection;
using System.Text;
using System.Linq;
using System.IO;
using TMPro;

public class CodeEditor : MonoBehaviour
{
    public TMP_InputField codeInputField;
    public Button compileButton;
    public TextMeshProUGUI outputText;

    private string defaultCode = @"
using UnityEngine;

public class DynamicScript : MonoBehaviour
{
    private string message;

    public void Run()
    {
        // Defina ou atualize o valor da variável 'message' aqui
        message = ""Hello, Universe!"";
    }

    public string GetMessage()
    {
        return message;
    }
}";

    private Assembly dynamicAssembly;
    private Type dynamicScriptType;
    private GameObject dynamicGameObject;
    private object dynamicScriptInstance;

    void Awake()
    {
        // Inicializar o InputField com o código padrão
        if (codeInputField != null)
        {
            codeInputField.text = defaultCode;
        }

        // Carregar o assembly padrão
        dynamicAssembly = CompileAssembly(defaultCode);
        dynamicScriptType = dynamicAssembly.GetType("DynamicScript");

        // Criar um GameObject
        dynamicGameObject = new GameObject("DynamicScript");
    }

    void Start()
    {
        if (compileButton != null)
        {
            compileButton.onClick.AddListener(CompileAndRunCode);
        }
    }

    void CompileAndRunCode()
    {
        string code = codeInputField.text;
        dynamicAssembly = CompileAssembly(code);
        dynamicScriptType = dynamicAssembly.GetType("DynamicScript");

        // Se houver um erro ao compilar, exibir mensagem de erro
        if (dynamicAssembly == null || dynamicScriptType == null)
        {
            outputText.text = "Erro ao compilar o código.";
            return;
        }

        // Remover o componente DynamicScript, se já existir
        if (dynamicGameObject.GetComponent(dynamicScriptType) != null)
        {
            Destroy(dynamicGameObject.GetComponent(dynamicScriptType));
        }

        // Adicionar o componente DynamicScript ao GameObject
        dynamicScriptInstance = dynamicGameObject.AddComponent(dynamicScriptType);

        // Chamar o método Run do script
        MethodInfo runMethod = dynamicScriptType.GetMethod("Run", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if (runMethod != null)
        {
            runMethod.Invoke(dynamicScriptInstance, null);

            // Obter a mensagem e atualizar o outputText
            MethodInfo getMessageMethod = dynamicScriptType.GetMethod("GetMessage", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (getMessageMethod != null)
            {
                string message = (string)getMessageMethod.Invoke(dynamicScriptInstance, null);
                outputText.text = message;
            }
            else
            {
                outputText.text = "Método GetMessage não encontrado!";
            }
        }
        else
        {
            outputText.text = "Método Run não encontrado!";
        }
    }

    Assembly CompileAssembly(string code)
    {
        // Configurar a sintaxe da árvore de código
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

        // Adicionar referências
        var references = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
            .Select(a => MetadataReference.CreateFromFile(a.Location))
            .ToList();

        // Adicionar referências específicas do Unity
        references.Add(MetadataReference.CreateFromFile(typeof(UnityEngine.Debug).Assembly.Location));
        references.Add(MetadataReference.CreateFromFile(typeof(UnityEngine.MonoBehaviour).Assembly.Location));

        CSharpCompilation compilation = CSharpCompilation.Create(
            "DynamicAssembly",
            new[] { syntaxTree },
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        // Compilar o código em memória
        using (var ms = new MemoryStream())
        {
            EmitResult result = compilation.Emit(ms);

            if (!result.Success)
            {
                StringBuilder errors = new StringBuilder();
                foreach (Diagnostic diagnostic in result.Diagnostics)
                {
                    errors.AppendLine($"{diagnostic.Id}: {diagnostic.GetMessage()}");
                }
                Debug.LogError(errors.ToString());
                return null;
            }

            // Carregar o assembly em memória
            ms.Seek(0, SeekOrigin.Begin);
            return Assembly.Load(ms.ToArray());
        }
    }
}
