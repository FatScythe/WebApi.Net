SELECT * FROM "Pokemon";

SELECT * FROM "Pokemon"
WHERE "Id" = 1;

SELECT * FROM "PokemonCategories";

SELECT * FROM "Pokemon" INNER JOIN "PokemonCategories" 
ON "Id" = "PokemonId";


SELECT * FROM "Pokemon" INNER JOIN "PokemonOwners" 
ON "Id" = "PokemonId";

DELETE FROM "Pokemon"
WHERE "Id" = 7;