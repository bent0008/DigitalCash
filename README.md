# DigitalCash




Ben Turnock

Digital Cash Implementation

Digital cash implementation by Bruce Schneier to securely and anonymously make virtual, financial transactions.
Using RSA blind signature, a money order will be encrypted by the customer using the public key, signed by the bank using the private key, unblinded by the customer, then decrypted by the merchant using the public key. The money order contains an amount, serial number, and 15 unique and random number pairs that when XOR'd, reveal the customer's ID