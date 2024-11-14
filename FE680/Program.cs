using Abbotware.Core.Collections.Trees;
using Abbotware.Core.Collections.Trees.Internal;
using Abbotware.Core.Serialization.Helpers;

{
    double sigma = 0.3637 / 100.0;     // Standard deviation for second image

    // Example usage for the first system with p and r_uu
    double rExpected1 = 5.5265 / 100.0; // Expected rate for p and r_uu (first image)
    double rLower1 = 5.1635 / 100.0;    // Lower rate (r_dd) for first image

    double rUu = BisectionMethod(r_uu => ComputeFunctionRuu(r_uu, rExpected1, rLower1, sigma), rExpected1, 0.10);
    double p = (rExpected1 - rLower1) / (rUu - rLower1);
    Console.WriteLine($"r^uu = {rUu * 100.0:F6}%, p = {p:F6}");

    // Example usage for the second system with q and r_dd
    double rExpected2 = 4.8006 / 100.0; // Expected rate for q and r_dd (second image)
    double rUpper2 = 5.1635 / 100.0;    // Upper rate (r_uu) for second image


    double rDd = BisectionMethod(r_dd => ComputeFunctionRdd(r_dd, rExpected2, rUpper2, sigma), 0.0, rUpper2);
    double q = (rExpected2 - rUpper2) / (rDd - rUpper2);
    Console.WriteLine($"r^dd = {rDd * 100.0:F6}%, q = {q:F6}");
}

var k = 0.025;
var θ = 0.15339;
var r0 = 0.05121;
var Δt = 1.0 / 12.0;
var σ = .0126;
var depth = 7u;

var rht = new RecombingHeapTree<Node<State>, State, double>(2, depth);
int nodeOrder = 0;

var expectedRates = new Dictionary<uint, double>();
expectedRates.Add(0, r0);

for (uint i = 1; i < depth; ++i)
{
    var rate = ExpectedRate(expectedRates[i - 1], k, θ, Δt);
    expectedRates.Add(i, rate);
}


var order = new Queue<ulong>();


rht.TraverseDownMiddleOut(
    root =>
    {
        InitializeNode(root);
        root.State.Rate = expectedRates[root.Depth];
    },
    m =>
    {
        InitializeNode(m);
        m.State.Rate = expectedRates[m.Depth];

    },
    onUpperInteriorNode: InitializeNode,
    onLowerInteriorNode: InitializeNode,
    onUpperFrontierNode: InitializeNode,
    onLowerFrontierNode: InitializeNode);


PrintTree(rht);

var currentId = 0;


for (var l = 0u; l < rht.Height; ++l)
{
    switch (l)
    {
        case 0:
            _ = order.Dequeue();
            currentId++;
            break;
        case 1:
            var up = rht[order.Dequeue()];
            up.State.Rate = RateUp(up.Parents.First().State.Rate!.Value, k, θ, σ, Δt);

            var down = rht[order.Dequeue()];
            down.State.Rate = RateDown(up.Parents.Last().State.Rate!.Value, k, θ, σ, Δt);
            break;
        case 2:
            // skip skip the middle node
            _ = rht[order.Dequeue()];

            // upper node first
            var L2_up = rht[order.Dequeue()];
            ApplyToUpperBranch(L2_up, k, θ, Δt, σ, rht);

            // lower node second
            var L2_down = rht[order.Dequeue()];
            ApplyToLowerBranch(L2_down, k, θ, Δt, σ, rht);
            break;
        case 3:
            up = rht[order.Dequeue()];
            up.State.Rate = RateUp(up.Parents[0].State.Rate!.Value, k, θ, σ, Δt);

            ApplyToUpperBranch(rht[order.Dequeue()], k, θ, Δt, σ, rht);

            down = rht[order.Dequeue()];
            down.State.Rate = RateDown(down.Parents[1].State.Rate!.Value, k, θ, σ, Δt);
            ApplyToLowerBranch(rht[order.Dequeue()], k, θ, Δt, σ, rht);
            break;

        case 4:
            // skip skip the middle node
            _ = rht[order.Dequeue()];

            ApplyToUpperBranch(rht[order.Dequeue()], k, θ, Δt, σ, rht);
            ApplyToUpperBranch(rht[order.Dequeue()], k, θ, Δt, σ, rht);

            ApplyToLowerBranch(rht[order.Dequeue()], k, θ, Δt, σ, rht);
            ApplyToLowerBranch(rht[order.Dequeue()], k, θ, Δt, σ, rht);
            break;

        case 5:
            up = rht[order.Dequeue()];
            up.State.Rate = RateUp(up.Parents[0].State.Rate!.Value, k, θ, σ, Δt);

            ApplyToUpperBranch(rht[order.Dequeue()], k, θ, Δt, σ, rht);
            ApplyToUpperBranch(rht[order.Dequeue()], k, θ, Δt, σ, rht);

            down = rht[order.Dequeue()];
            down.State.Rate = RateDown(down.Parents[1].State.Rate!.Value, k, θ, σ, Δt);

            ApplyToLowerBranch(rht[order.Dequeue()], k, θ, Δt, σ, rht);
            ApplyToLowerBranch(rht[order.Dequeue()], k, θ, Δt, σ, rht);
            break;
        case 6:
            // skip skip the middle node
            _ = rht[order.Dequeue()];

            ApplyToUpperBranch(rht[order.Dequeue()], k, θ, Δt, σ, rht);
            ApplyToUpperBranch(rht[order.Dequeue()], k, θ, Δt, σ, rht);
            ApplyToUpperBranch(rht[order.Dequeue()], k, θ, Δt, σ, rht);

            ApplyToLowerBranch(rht[order.Dequeue()], k, θ, Δt, σ, rht);
            ApplyToLowerBranch(rht[order.Dequeue()], k, θ, Δt, σ, rht);
            ApplyToLowerBranch(rht[order.Dequeue()], k, θ, Δt, σ, rht);
            break;

        case 7:
            // skip skip the middle node
            _ = rht[order.Dequeue()];

            ApplyToUpperBranch(rht[order.Dequeue()], k, θ, Δt, σ, rht);
            ApplyToUpperBranch(rht[order.Dequeue()], k, θ, Δt, σ, rht);
            ApplyToUpperBranch(rht[order.Dequeue()], k, θ, Δt, σ, rht);

            ApplyToLowerBranch(rht[order.Dequeue()], k, θ, Δt, σ, rht);
            ApplyToLowerBranch(rht[order.Dequeue()], k, θ, Δt, σ, rht);
            ApplyToLowerBranch(rht[order.Dequeue()], k, θ, Δt, σ, rht);
            break;
    }
}

PrintTree(rht);

PrintMermaid(rht);


string PrintNode(Node<State> r)
{
    var weight = r.Depth * r.Id;
    var probs = string.Empty;

    switch (r.ParentIds.Count)
    {
        case 0: // root
            break;
        case 1: // frontier node
            var e0 = CreateEdge(r.Parents[0], r);
            var p0 = rht.Edges[CreateEdge(r.Parents[0], r)];
            probs += $"{p0:n7} % ({e0:n7})";
            break;
        case 2: // interier node
            var e2 = CreateEdge(r.Parents[1], r);
            var p2 = rht.Edges[CreateEdge(r.Parents[1], r)];
            probs += $" ({e2:n7}): {p2:n7} %";

            var e1 = CreateEdge(r.Parents[0], r);
            var p1 = rht.Edges[CreateEdge(r.Parents[0], r)];
            probs += $" ({e1:n7}): {p1:n7} %";
            break;
        default:
            throw new Exception();
    }

    var line = $"{new string(' ', 2 * (int)r.Depth)}ID:{r.Id} Rate:{r.State.Rate:n5}  {probs}";
    return line;
}

static double RateUp(double r, double k, double θ, double σ, double Δt)
    => ExpectedRate(r, k, θ, Δt) + Std(σ, Δt);

static double RateDown(double r, double k, double θ, double σ, double Δt)
    => ExpectedRate(r, k, θ, Δt) - Std(σ, Δt);

static double ExpectedRate(double r0, double k, double θ, double Δt)
    => r0 + (k * (θ - r0) * Δt);

static double Std(double σ, double Δt)
    => σ * Math.Sqrt(Δt);

static string CreateEdge(Node<State> parent, Node<State> child)
    => $"{parent.Id}->{child.Id}";


void InitializeNode(Node<State> m)
{
    order.Enqueue(m.Id);
    m.State.OrderVisited = ++nodeOrder;


    switch (m.ParentIds.Count)
    {
        case 0: // root
            break;
        case 1: // frontier node
            rht.Edges.Add(CreateEdge(m.Parents[0], m), .5);
            break;
        case 2: // interier node
            rht.Edges.Add(CreateEdge(m.Parents[0], m), .5);
            rht.Edges.Add(CreateEdge(m.Parents[1], m), .5);
            break;
        default:
            throw new Exception();
    }
}



void PrintTree(RecombingHeapTree<Node<State>, State, double> rht)
{
    for (var l = 0u; l < rht.Height; ++l)
    {
        foreach (var id in rht.Levels[l].OrderByDescending(x => x))
        {
            var n = rht[id];
            Console.WriteLine(PrintNode(n));
        }
    }
}

void PrintMermaid(RecombingHeapTree<Node<State>, State, double> rht)
{
    for (var l = 0u; l < rht.Height - 1; ++l)
    {
        var nodes = rht.Levels[l];
        foreach(var n in nodes)
        {
            Console.WriteLine($"{rht[n].State.Rate:n5} -->|{rht.Edges[CreateEdge(rht[n], rht[n].Children[1])]:n5} | {rht[n].Children[1].State.Rate:n5}");
            Console.WriteLine($"{rht[n].State.Rate:n5} -->|{rht.Edges[CreateEdge(rht[n], rht[n].Children[0])]:n5} | {rht[n].Children[0].State.Rate:n5}");
        }

    }
}


static (double r_uu, double p) SolveSystemUp(double μ, double rLower, double σ)
{
    //Console.WriteLine($"μ = {μ} rLower = {rLower} σ = {σ}");

    double r = BisectionMethod(r_uu => ComputeFunctionRuu(r_uu, μ, rLower, σ), μ - .2, 0.10);
    double p = (μ - rLower) / (r - rLower);
    //Console.WriteLine($"Solution for first system: r^uu = {r * 100.0:F6}%, p = {p:F6}");
    return (r, p);
}
static (double r_dd, double q) SolveSystemDown(double μ, double rUpper, double σ)
{
    // Console.WriteLine($"μ = {μ} rLower = {rUpper} σ = {σ}");

    double r = BisectionMethod(r_dd => ComputeFunctionRdd(r_dd, μ, rUpper, σ), 0.0, rUpper);
    double q = (μ - rUpper) / (r - rUpper);
    //Console.WriteLine($"Solution for second system: r^dd = {r * 100.0:F6}%, q = {q:F6}");
    return (r, q);
}

static void ApplyToUpperBranch(Node<State> L2_up, double k, double θ, double Δt, double σ, RecombingHeapTree<Node<State>, State, double> rht)
{
    var lower_parent = L2_up.Parents[0];

    if (L2_up.ParentIds.Count > 1)
    {
        lower_parent = L2_up.Parents[0];
    }

    var lower_parent_lower_child = lower_parent.Children[0];

    var r0 = lower_parent.State.Rate!.Value;
    var rd = lower_parent_lower_child.State.Rate!.Value;
    var μ = ExpectedRate(r0, k, θ, Δt);

    (L2_up.State.Rate, var p) = SolveSystemUp(μ, rd, Std(σ, Δt));
    //(L2_up.State.Rate, var p) = SolveUsingGridSearch(μ, rd, Std(σ, Δt));


    rht.Edges[CreateEdge(lower_parent, L2_up)] = p;
    rht.Edges[CreateEdge(lower_parent, lower_parent_lower_child)] = 1 - p;
}

static void ApplyToLowerBranch(Node<State> L2_down, double k, double θ, double Δt, double σ, RecombingHeapTree<Node<State>, State, double> rht)
{
    var upper_parent = L2_down.Parents[0];

    if (L2_down.ParentIds.Count > 1)
    {
        upper_parent = L2_down.Parents[1];
    }

    var upper_parent_upper_child = upper_parent.Children[1];

    var r0 = upper_parent.State.Rate!.Value;
    var ruu = upper_parent_upper_child.State.Rate!.Value;
    var μ = ExpectedRate(r0, k, θ, Δt);

    (L2_down.State.Rate, var q) = SolveSystemDown(μ, ruu, Std(σ, Δt));

    rht.Edges[CreateEdge(upper_parent, L2_down)] = q;
    rht.Edges[CreateEdge(upper_parent, upper_parent_upper_child)] = 1 - q;
}



static double BisectionMethod(Func<double, double> function, double lowerBound, double upperBound, double tolerance = 1e-12, int maxIterations = 1000)
{
    // Implement the bisection method for root finding
    for (int i = 0; i < maxIterations; i++)
    {
        double mid = (lowerBound + upperBound) / 2.0;
        double fMid = function(mid);

        if (Math.Abs(fMid) < tolerance)
        {
            return mid; // Root found
        }

        double fLower = function(lowerBound);

        if (fLower * fMid < 0)
        {
            upperBound = mid;
        }
        else
        {
            lowerBound = mid;
        }
    }

    // If no root is found within maxIterations
    return double.NaN;
}

static double ComputeFunctionRuu(double r_uu, double μ, double rLower, double σ)
{
    // first equation
    double p = (μ - rLower) / (r_uu - rLower);

    // Substitute p into second equation
    double term1 = p * Math.Pow(r_uu - μ, 2);
    double term2 = (1 - p) * Math.Pow(rLower - μ, 2);
    double value = term1 + term2 - Math.Pow(σ, 2);

    return value;
}

static double ComputeFunctionRdd(double r_dd, double μ, double rUpper, double σ)
{
    // first equation
    double q = (μ - rUpper) / (r_dd - rUpper);

    // Substitute q into second equation
    double a = q * Math.Pow(rUpper - μ, 2);
    double b = (1 - q) * Math.Pow(r_dd - μ, 2);
    double value = a + b - Math.Pow(σ, 2);

    return value;
}

