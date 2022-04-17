import Header from './heading'
type LayoutProps = {
  children: React.ReactNode,
};

export default function Layout({ children }: LayoutProps) {
  return (
    <div className="container">
      <Header />
      <main className="container max-w-3xl">{children}</main>
    </div>
  )
}
