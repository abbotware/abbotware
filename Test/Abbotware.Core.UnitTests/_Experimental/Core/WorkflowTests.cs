namespace Abbotware.UnitTests.Core
{
    using System;
    using System.Linq;
    using Abbotware.Core.Workflow;
    using Abbotware.Core.Workflow.ExtensionPoints;
    using Abbotware.Core.Workflow.Plugins;
    using Abbotware.Interop.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Experimental")]
    public class WorkflowTests
    {
        [Test]
        public void AddState()
        {
            var fsm = this.EmptyStateMachine() as IStateMachineManager;
            {
                var s = fsm.AddState("New");

                Assert.AreEqual(1, s.Id);
                Assert.AreEqual("New", s.Name);
                Assert.AreEqual(false, s.IsStart);

                var a = fsm.GetStates();

                Assert.AreEqual(1, a.Count());
            }

            {
                var s = fsm.AddState("New 2");

                Assert.AreEqual(2, s.Id);
                Assert.AreEqual("New 2", s.Name);
                Assert.AreEqual(false, s.IsStart);

                var a = fsm.GetStates();

                Assert.AreEqual(2, a.Count());
            }
        }

        [Test]
        [ExpectedException(typeof(DuplicateStateException))]
        public void AddStateTwice()
        {
            var fsm = this.EmptyStateMachine() as IStateMachineManager;

            fsm.AddState("New");
            fsm.AddState("New");
        }

        [Test]
        public void VerifyNullState()
        {
            var fsm = this.EmptyStateMachine() as IStateMachineManager;
            var state = fsm.AddState(string.Empty);

            Assert.IsNotNull(state);
            Assert.AreSame(State.NullState, state);
        }

        [Test]
        [ExpectedException(typeof(DuplicateStateException))]
        public void AddDuplicateState()
        {
            var wf = this.EmptyStateMachine() as IStateMachineManager;

            wf.AddState(new State(string.Empty, 1, false));
            wf.AddState(new State(string.Empty, 2, false));
        }

        [Test]
        [ExpectedException(typeof(DuplicateStateException))]
        public void AddDuplicateStateCaseInsensitive()
        {
            var wf = this.EmptyStateMachine() as IStateMachineManager;

            wf.AddState(new State("test", 1, false));
            wf.AddState(new State("TEST", 2, false));
        }

        [Test]
        [ExpectedException(typeof(DuplicateStateException))]
        public void AddDuplicateStateWithId()
        {
            var wf = this.EmptyStateMachine() as IStateMachineManager;

            wf.AddState(new State("sadf", 123, false));
            wf.AddState(new State("sadssf", 123, false));
        }

        [Test]
        public void AddActions()
        {
            var wf = this.EmptyStateMachine() as IStateMachineManager;

            var s = wf.AddState("source");
            var t = wf.AddState("target");

            var a1 = wf.AddAction("s2t1", s, t);
            var a2 = wf.AddAction("s2t2", s, t);
            var a3 = wf.AddAction("t2s", t, s);

            var sa = wf.GetActions("source");

            Assert.AreEqual(2, sa.Count());
            Assert.IsTrue(sa.Any(x => x.Name == "s2t1"));
            Assert.IsTrue(sa.Any(x => x.Name == "s2t2"));
            Assert.IsTrue(sa.All(x => object.ReferenceEquals(s, x.Source)));
            Assert.IsTrue(sa.All(x => object.ReferenceEquals(t, x.Target)));

            var ta = wf.GetActions(t);

            Assert.AreEqual(1, ta.Count());
            Assert.IsTrue(ta.Any(x => x.Name == "t2s"));
            Assert.IsTrue(ta.All(x => object.ReferenceEquals(t, x.Source)));
            Assert.IsTrue(ta.All(x => object.ReferenceEquals(s, x.Target)));
        }

        [Test]
        [ExpectedException(typeof(DuplicateActionException))]
        public void AddActionTwice()
        {
            var fsm = this.EmptyStateMachine() as IStateMachineManager;

            fsm.AddAction("New", "Test", "Test1");
            fsm.AddAction("New", "Test", "Test1");
        }

        [Test]
        [ExpectedException(typeof(DuplicateActionException))]
        public void AddSelfActionTwice()
        {
            var fsm = this.EmptyStateMachine() as IStateMachineManager;

            // Self Action twice
            fsm.AddAction("New", "Test", "Test");
            fsm.AddAction("New", "Test", "Test");
        }

        [Test]
        [ExpectedException(typeof(DuplicateActionException))]
        public void AddDuplicateActionWithId()
        {
            var wf = this.EmptyStateMachine() as IStateMachineManager;

            var s = wf.AddState("source");
            var t = wf.AddState("target");

            wf.AddAction(new ActionEdge("test1", 1, s, t));
            wf.AddAction(new ActionEdge("test2", 1, s, t));
        }

        [Test]
        [ExpectedException(typeof(DuplicateActionException))]
        public void AddSelfActionTwiceCaseInsensitive()
        {
            var fsm = this.EmptyStateMachine() as IStateMachineManager;

            // Self Action twice
            fsm.AddAction("New", "Test", "Test");
            fsm.AddAction("new", "Test", "Test");
        }

        [Test]
        [ExpectedException(typeof(StateNotFoundException))]
        public void ApplyAction_InvalidState()
        {
            var fsm = this.EmptyStateMachine() as IStateMachineManager;

            fsm.ApplyAction("test", "test");
        }

        [Test]
        [ExpectedException(typeof(ActionNotFoundException))]
        public void ApplyAction_InvalidAction()
        {
            var fsm = this.EmptyStateMachine() as IStateMachineManager;
            fsm.AddState("test");
            fsm.ApplyAction("test", "test");
        }

        [Test]
        public void ApplyAction()
        {
            var fsm = this.EmptyStateMachine() as IStateMachineManager;

            fsm.AddState("s1");
            fsm.AddState("s2");
            fsm.AddAction("a1", "s1", "s2");

            var a = fsm.ApplyAction("s1", "a1");

            Assert.AreEqual("a1", a.Name);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ApplyAction_NullState1()
        {
            var fsm = this.EmptyStateMachine();
            fsm.ApplyAction(default(IState), "action");
        }

        [Test]
        [ExpectedException(typeof(StateNotFoundException))]
        public void ApplyAction_NullState2()
        {
            var fsm = this.EmptyStateMachine();
            fsm.ApplyAction(default(string), "action");
        }

        [Test]
        [ExpectedException(typeof(ActionNotFoundException))]
        public void ApplyAction_NullAction1()
        {
            var fsm = this.EmptyStateMachine();
            fsm.AddState("test");
            fsm.ApplyAction("test", null);
        }

        [Test]
        [ExpectedException(typeof(ActionNotFoundException))]
        public void ApplyAction_NullAction2()
        {
            var fsm = this.EmptyStateMachine();
            fsm.AddState("test");
            var s = fsm.GetState("test");

            fsm.ApplyAction(s, null);
        }

        [Test]
        public void StateMachineT_ApplyAction()
        {
            var sm = this.StateMachineT();

            var sc = new SimpleClass();
            {
                Assert.AreEqual("new", sc.State);
                var a = sm.DetermineActions(sc);
                Assert.AreEqual(1, a.Count());
                Assert.AreEqual("commit", a.Single().Name);
            }

            sm.ApplyAction(sc, "commit");
            {
                Assert.AreEqual("committed", sc.State);
                var a = sm.DetermineActions(sc);
                Assert.AreEqual(2, a.Count());
                Assert.IsNotNull(a.Single(x => x.Name == "settle"));
                Assert.IsNotNull(a.Single(x => x.Name == "revert"));
            }

            sm.ApplyAction(sc, "settle");
            {
                Assert.AreEqual("settled", sc.State);
                var a = sm.DetermineActions(sc);
                Assert.AreEqual(1, a.Count());
                Assert.IsNotNull(a.Single(x => x.Name == "revert"));
            }

            sm.ApplyAction(sc, "revert");
            {
                Assert.AreEqual("committed", sc.State);
                var a = sm.DetermineActions(sc);
                Assert.AreEqual(2, a.Count());
                Assert.IsNotNull(a.Single(x => x.Name == "settle"));
                Assert.IsNotNull(a.Single(x => x.Name == "revert"));
            }

            sm.ApplyAction(sc, "revert");
            {
                Assert.AreEqual("new", sc.State);
                var a = sm.DetermineActions(sc);
                Assert.AreEqual(1, a.Count());
                Assert.AreEqual("commit", a.Single().Name);
            }
        }

        [Test]
        [ExpectedException(typeof(ActionNotFoundException))]
        public void StateMachineT_ApplyAction_InvalidAction()
        {
            var sm = this.StateMachineT();

            var sc = new SimpleClass();

            sm.ApplyAction(sc, "DoesNotExist");
        }

        [Test]
        public void Workflow_GetActionsNewObject()
        {
            ////var fsm = BuildStateMachine();

            ////var c = new SimpleOrder();

            ////var actions = fsm.DetermineActions(c);

            ////Assert.IsNotNull(actions);
            ////Assert.AreEqual(1, actions.Count());

            ////var state = fsm.DetermineState(c);

            ////Assert.IsNotNull(state);
            ////Assert.ReferenceEquals(WorkflowState.NullState, state);
        }

        [Test]
        public void Workflow_AddActions()
        {
            ////var wf = EmptyStateMachine();

            ////var fsm = wf as IWorkflowStateMachineManager<SimpleOrder>;

            //////Action
            ////fsm.AddAction("New", "Test", "Test1");
            ////fsm.AddAction("Update", "Test", "Test");

            ////var actions = wf.DetermineActions(new SimpleOrder { Name = "test", Status = "Test" });

            ////Assert.AreEqual(2, actions.Count());

            ////var firstAction = actions.Single(x => x.Name == "New");
            ////Assert.AreEqual("Test", firstAction.Source.Name);
            ////Assert.AreEqual("Test1", firstAction.Target.Name);

            ////var SecondAction = actions.Single(x => x.Name == "Update");
            ////Assert.AreEqual("Test", SecondAction.Source.Name);
            ////Assert.AreEqual("Test", SecondAction.Target.Name);
        }

        protected virtual IStateMachineManager EmptyStateMachine()
        {
            return new StateMachine("test", 1);
        }

        protected virtual IStateMachine<SimpleClass> StateMachineT()
        {
            var sm = new StateMachine<SimpleClass>((x) => x.State, (x, s) => { x.State = s; }, "SimpleClass", 1);

            var smm = sm as IStateMachineManager;

            smm.AddState("new");
            smm.AddState("committed");
            smm.AddState("settled");

            smm.AddAction("commit", "new", "committed");
            smm.AddAction("revert", "committed", "new");

            smm.AddAction("settle", "committed", "settled");
            smm.AddAction("revert", "settled", "committed");

            return sm;
        }

        public class SimpleClass
        {
            public string State { get; set; } = "new";

            public int Id { get; set; }
        }
    }
}