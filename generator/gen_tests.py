import yaml
import os
from pathlib import Path

# Шаблон файла C#
TEMPLATE = """// AUTO-GENERATED DO NOT EDIT
using System;
using NUnit.Framework;
using Lab.Interfaces;
using Lab.Implementations.GenCode1; 

namespace Module.Tests
{{
    [TestFixture]
    public class GeneratedTests
    {{
        private Lab.Interfaces.IBinaryToDecimalConverter _sut;

        [SetUp]
        public void Setup() 
        {{
            _sut = new Lab.Implementations.GenCode1.BinaryToDecimalConverter();
        }}

        {methods}
    }}
}}"""

def generate():
    with open("config.yaml", 'r') as f:
        cfg = yaml.safe_load(f)
    with open(cfg['spec_path'], 'r') as f:
        spec = yaml.safe_load(f)

    test_methods = ""
    for m in spec['methods']:
        for eq in m['equivalence_classes']:
            inp = eq['inputs'][0]
            # Форматируем входной параметр для C#
            val = f'"{inp}"' if isinstance(inp, str) else str(inp)
            
            # Обработка ожидаемого результата (Exception или Значение)
            if eq['expected'] == "ArgumentException":
                action = f"Assert.Throws<ArgumentException>(() => _sut.{m['name']}({val}));"
            else:
                # Если ожидаем строку, добавляем кавычки, если число - оставляем как есть
                expected_val = eq['expected']
                if isinstance(expected_val, str):
                    formatted_expected = f'"{expected_val}"'
                else:
                    formatted_expected = str(expected_val)
                
                action = f"var res = _sut.{m['name']}({val});\n            Assert.That(res, Is.EqualTo({formatted_expected}));"

            test_methods += f"""
        [Test]
        public void {m['name']}_{eq['case'].replace(" ", "_")}_Test()
        {{
            {action}
        }}\n"""

    full_code = TEMPLATE.format(methods=test_methods)
    out_dir = Path(cfg['output_dir'])
    out_dir.mkdir(parents=True, exist_ok=True)
    (out_dir / "GeneratedTests.cs").write_text(full_code, encoding='utf-8')

if __name__ == "__main__":
    generate()
