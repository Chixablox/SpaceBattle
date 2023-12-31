﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace SpaceBattle.Tests.ExceptionHandleTests
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class ОбработчкаОшибокFeature : object, Xunit.IClassFixture<ОбработчкаОшибокFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "ExceptionHadleTests.feature"
#line hidden
        
        public ОбработчкаОшибокFeature(ОбработчкаОшибокFeature.FixtureData fixtureData, SpaceBattle_Tests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("ru-RU"), "ExceptionHandleTests", "Обработчка ошибок", null, ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Обработка известной дереву ошибки, вызванной известной дереву командой")]
        [Xunit.TraitAttribute("FeatureTitle", "Обработчка ошибок")]
        [Xunit.TraitAttribute("Description", "Обработка известной дереву ошибки, вызванной известной дереву командой")]
        public void ОбработкаИзвестнойДеревуОшибкиВызваннойИзвестнойДеревуКомандой()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Обработка известной дереву ошибки, вызванной известной дереву командой", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 3
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
        testRunner.Given("во время выполнения команды возникло исключение", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Дано ");
#line hidden
#line 5
        testRunner.When("исключение типа SetPosEx, вызванное командой типа MoveCommand, обрабатывается", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Когда ");
#line hidden
#line 6
        testRunner.Then("срабатывает первый обработчик", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Тогда ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Обработка неизвестной дереву ошибки, вызванной известной дереву командой")]
        [Xunit.TraitAttribute("FeatureTitle", "Обработчка ошибок")]
        [Xunit.TraitAttribute("Description", "Обработка неизвестной дереву ошибки, вызванной известной дереву командой")]
        public void ОбработкаНеизвестнойДеревуОшибкиВызваннойИзвестнойДеревуКомандой()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Обработка неизвестной дереву ошибки, вызванной известной дереву командой", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 8
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 9
        testRunner.Given("во время выполнения команды возникло исключение", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Дано ");
#line hidden
#line 10
        testRunner.When("исключение неизвестного типа, вызванное командой типа MoveCommand, обрабатывается" +
                        "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Когда ");
#line hidden
#line 11
        testRunner.Then("срабатывает второй обработчик", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Тогда ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Обработка известной дереву ошибки, вызванной неизвестной дереву командой")]
        [Xunit.TraitAttribute("FeatureTitle", "Обработчка ошибок")]
        [Xunit.TraitAttribute("Description", "Обработка известной дереву ошибки, вызванной неизвестной дереву командой")]
        public void ОбработкаИзвестнойДеревуОшибкиВызваннойНеизвестнойДеревуКомандой()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Обработка известной дереву ошибки, вызванной неизвестной дереву командой", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 13
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 14
        testRunner.Given("во время выполнения команды возникло исключение", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Дано ");
#line hidden
#line 15
        testRunner.When("исключение типа SetPosEx, вызванное командой неизвестного типа, обрабатывается", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Когда ");
#line hidden
#line 16
        testRunner.Then("срабатывает третий обработчик", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Тогда ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Обработка неизвестной дереву ошибки, вызванной неизвестной дереву командой")]
        [Xunit.TraitAttribute("FeatureTitle", "Обработчка ошибок")]
        [Xunit.TraitAttribute("Description", "Обработка неизвестной дереву ошибки, вызванной неизвестной дереву командой")]
        public void ОбработкаНеизвестнойДеревуОшибкиВызваннойНеизвестнойДеревуКомандой()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Обработка неизвестной дереву ошибки, вызванной неизвестной дереву командой", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 18
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 19
        testRunner.Given("во время выполнения команды возникло исключение", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Дано ");
#line hidden
#line 20
        testRunner.When("исключение неизвестного типа, вызванное командой неизвестного типа, обрабатываетс" +
                        "я", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Когда ");
#line hidden
#line 21
        testRunner.Then("срабатывает четвёртый обработчик", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Тогда ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                ОбработчкаОшибокFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                ОбработчкаОшибокFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
