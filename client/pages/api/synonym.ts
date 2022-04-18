// Next.js API route support: https://nextjs.org/docs/api-routes/introduction
import type { NextApiRequest, NextApiResponse } from 'next'
import axios from 'axios'

type Word = {
    word: string,
    synonyms: string[]
}
// {
//   "firstWord": "string",
//   "secondWord": "string"
// }
type CreateSynonymRequest = {
    firstWord: string,
    secondWord: string
}

export default async function handler(req: NextApiRequest, res: NextApiResponse) {
    console.log("handler called with req method: " + req.method);
    const baseUrl = `http://${process.env.API_HOST}:${process.env.API_PORT}`;
    if (req.method === 'GET') {
        console.log("GET");
        const word = req.query.word as string;
        const resp = await axios.get<Word>(`${baseUrl}/Synonym/${word}`);
        res.status(200).json(resp.data);
    } else if (req.method === 'POST') {
        console.log("POST");
        const request = req.body as CreateSynonymRequest;
        const resp = await axios.post<CreateSynonymRequest>(`${baseUrl}/Synonym`, request);
        res.status(200).json(resp.status);
    }
}
