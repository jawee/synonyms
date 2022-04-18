import Document, { Html, Head, Main, NextScript } from 'next/document';

export default class MyDocument extends Document {
  render() {
    return (
      <Html>
        <Head>
            <title>synonyms</title>
        </Head>
        <body className="bg-[#2F3E46]">
          <Main />
          <NextScript />
        </body>
      </Html>
    );
  }
}
