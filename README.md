# Negative-Association-Rules-Miner

## What is negative association rule about?
Typically association rule mining only considers positive frequent itemsets in rule generation, where rules involving only the presence of items are generated. In this paper we consider the complementary problem of negative association rule mining, which generates rules describing the absence of itemsets from transactions.

Typically, we will consider rules of following forms:
`A => ¬B`
`¬A => B`
`¬A => ¬A`

Where A is an antecedent of the rule and B - consequent of the rule. 
Both A and B represent the set of items in current transaction. 

## Approach of mining negative association rules
There are several algorithms proposed to induce negative association rules from a transaction set. Efficient discovery of such rules has been a major focus in the data mining research. From the celebrated Apriori algorithm there have been a remarkable number of variants and improvements of association rule mining algorithms. We will focus on two of them.  

---

### Algorithm 1 MPNAR
In the most algorithms they use only support-confidence framewrok. In the following algorithm we will extend the set of parameters by **conviction**. The purpose that we have introduced additional parameter is quite obvious - we do not want some potenatilly rules to disappear.
The conviction rule is defined as follows: 
```
conv(A=>B) = (1 - supp(B))/(1 - conf(A=>B))
```
###### Pseudocode
```
NAR = set of negative association rules. 
for (k=2; Fk-1!=Φ; k++)
{
  Ck= Fk-1 ⋈ Fk-1
  // Prune using Apriori Property
  for each i ε Ck, 
    for each i ε Ck
    {
      s= Support( i);
      for each A,B (A U B= i )
      {
        if ( Supp(A -> ┐B) ≥ MS && Conviction(A->┐B)≤2.0)
            addNAR <- { A ┐B)

        if ( Supp(┐A -> B) ≥ MS && Conviction(┐A -> B) ≤2.0) 
            addNAR <- { ┐A  B}
       }
    }
} 
```

Reference
[1]: http://www.enggjournals.com/ijet/docs/IJET11-03-02-23.pdf
