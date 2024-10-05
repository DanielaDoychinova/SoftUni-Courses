function censoredWords(str, word) {
  let result = str;
  let censured = '*'.repeat(word.length);
  while (result.includes(word)){
    result = result.replace(word, censured)
  }

      return result;
    }
    
    
