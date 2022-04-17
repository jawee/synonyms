import Header from './heading'

export default function Layout({ children }) {
  return (
    <div className="container">
      <Header />
      <main className="container max-w-3xl">{children}</main>
    </div>
  )
}
