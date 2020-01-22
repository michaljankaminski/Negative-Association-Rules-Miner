# Negative-Association-Rules-Miner

## What is negative association rule about?
Typically association rule mining only considers positive frequent itemsets in rule generation, where rules involving only the presence of items are generated. In this paper we consider the complementary problem of negative association rule mining, which generates rules describing the absence of itemsets from transactions.

## Approach of mining negative association rules
There are several algorithms proposed to induce negative association rules from a transaction set. Apriori is not a well-suited algorithm for negative association rule mining. In order to use apriori, every transaction needs to be updated with all the items—those that are present in the transaction and those that are absent. That cannot be considered as optimal and efficient solution. 

###### Algorithm
